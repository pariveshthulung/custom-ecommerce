namespace Ecommerce.Infrastructure.DependencyResolution;

public static class DependencyExtension
{
    public static IServiceCollection AddEcommerceInfrastructure(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration
    )
    {
        services.Configure<EcommerceInfrastructureSettings>(
            configuration.GetSection("EcommerceInfrastructureSettings")
        );
        // services.AddDbContext<EcommerceDbContext>(options =>
        //     options.UseSqlServer(configuration.GetConnectionString("EcommerceDbContext"))
        // );

        services.AddSingleton<ConvertDomainEventIntoOutboxMessageInterceptor>();

        services.AddDbContext<EcommerceDbContext>(
            (sp, options) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventIntoOutboxMessageInterceptor>();
                options
                    .BuildReadOptimizedDbContext(
                        environment,
                        configuration.GetConnectionString("EcommerceDbContext")!,
                        typeof(DependencyExtension).Assembly.GetName().Name!
                    )
                    .AddInterceptors(interceptor!);
            }
        );
        services.AddScoped<IDbConnection>(sp => new SqlConnection(
            configuration.GetConnectionString("EcommerceDbContext")
        ));
        services.AddHttpContextAccessor();

        services
            .AddScoped<ICartRepository, CartRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IAppUserRepository, AppUserRepository>()
            .AddScoped<IAppUserReadonlyRepository, AppUserReadonlyRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IProductConfirmRepository, ProductConfirmRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IStoreRepository, StoreRepository>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<ISeederRepository, SeederRepository>()
            .AddScoped<IReadonlyStoreRepository, ReadonlyStoreRepository>()
            .AddScoped<ILogRepository, LogRepository>();

        return services;
    }

    public static IServiceCollection AddQuartz(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutBoxMessageJob));
            configure
                .AddJob<ProcessOutBoxMessageJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(10).RepeatForever()
                        )
                );
        });
        services.AddQuartzHostedService();
        return services;
    }
}
