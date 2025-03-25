using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Ecommerce.Shared.Extension;

public static class IHostExtension
{
    public static async Task<IHost> MigrateDbContext<TContext>(
        this IHost host,
        Action<TContext, IServiceProvider> seeder
    )
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetService<TContext>();
        var logger = services.GetService<ILogger<TContext>>();
        var retries = 5;
        try
        {
            var retryPolicy = Policy
                .Handle<SqlException>()
                .WaitAndRetry(
                    retries,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)),
                    (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(
                            exception,
                            "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}",
                            nameof(TContext),
                            exception.GetType().Name,
                            exception.Message,
                            retry,
                            retries
                        );
                    }
                );
            retryPolicy.Execute(() =>
            {
                context.Database.Migrate();
                seeder(context, services);
            });

            return host;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred when migrating the database.");
            throw;
        }
    }
}
