namespace Ecommerce.Infrastructure.Data;

public class EcommerceDbContextSeeder
{
    public async Task SeedAsync(
        EcommerceDbContext context,
        IWebHostEnvironment evn,
        IOptions<EcommerceInfrastructureSetting> settings,
        ILogger<EcommerceDbContextSeeder> logger
    )
    {
        var policy = Policy
            .Handle<SqlException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(
                        exception,
                        "Exception {Exceptiontype} with message {Message} detected on attempt.",
                        exception.GetType(),
                        exception.Message
                    );
                }
            );

        await policy.ExecuteAsync(async () =>
        {
            using (context)
            {
                if (settings.Value.EnableMigrationSeed)
                {
                    // do migrations
                    await _MigrateEnumeration(context, context.UserTypeEnums);
                }
            }
        });
    }

    private async Task _MigrateEnumeration<T>(EcommerceDbContext context, DbSet<T> entity)
        where T : Enumeration
    {
        var dbEnumerations = (await entity.ToListAsync()) ?? Enumerable.Empty<T>();
        var localEnumerations = Enumeration
            .GetAll<T>()
            .Where(c => !dbEnumerations.Select(l => l.Id).Contains(c.Id));
        if (localEnumerations.Any())
        {
            foreach (var localEnumeration in localEnumerations)
                await context.AddAsync<T>(localEnumeration);

            await context.SaveChangesAsync();
        }
    }
}
