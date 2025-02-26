namespace Ecommerce.Shared.Infrastructure;

public static class DbContextExtentions
{
    public static DbContextOptionsBuilder BuildReadOptimizedDbcontext(
        this DbContextOptionsBuilder builder,
        bool enableSensitiveLogging,
        string connectionString
    ) => builder.BuildReadOptimizedDbContextPrivate(enableSensitiveLogging, connectionString, "");

    public static DbContextOptionsBuilder BuildReadOptimizedDbcontext(
        this DbContextOptionsBuilder builder,
        IHostEnvironment environment,
        string connectionString
    ) =>
        builder.BuildReadOptimizedDbContextPrivate(
            environment.IsDevelopment(),
            connectionString,
            ""
        );

    public static DbContextOptionsBuilder BuildReadOptimizedDbContext(
        this DbContextOptionsBuilder builder,
        IHostEnvironment enviroment,
        string connectionString,
        string migrationAssembly
    ) =>
        builder.BuildReadOptimizedDbContextPrivate(
            enviroment.IsDevelopment(),
            connectionString,
            migrationAssembly
        );

    private static DbContextOptionsBuilder BuildReadOptimizedDbContextPrivate(
        this DbContextOptionsBuilder builder,
        bool enableSensitiveLogging,
        string connectionString,
        string migrationAssembly
    )
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString));
        builder.UseSqlServer(
            connectionString,
            options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 15,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                if (!string.IsNullOrWhiteSpace(migrationAssembly))
                    options.MigrationsAssembly(migrationAssembly);

                options.MigrationsHistoryTable("_EFMigrationsHistory", "efcore");
            }
        );
#if !DEBUGNOLOG
        if (enableSensitiveLogging)
        {
            builder.EnableSensitiveDataLogging(true);
            builder.EnableDetailedErrors(true);
        }
#endif
        // by default disable change tracking for optimized query
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        return builder;
    }
}
