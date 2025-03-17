using System.Data;
using System.Reflection;
using Ecommerce.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Ecommerce.Infrastructure.DependencyResolution;

public static class DependencyExtension
{
    public static IServiceCollection AddEcommerceInfrastructure(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration
    )
    {
        services.Configure<EcommerceInfrastructureSetting>(
            configuration.GetSection("EcommerceInfrastructureSetting:Infrastructure")
        );
        // services.AddDbContext<EcommerceDbContext>(options =>
        //     options.UseSqlServer(configuration.GetConnectionString("EcommerceDbContext"))
        // );
        services.AddDbContext<EcommerceDbContext>(
            options =>
                options.BuildReadOptimizedDbContext(
                    environment,
                    configuration.GetConnectionString("EcommerceDbContext")!,
                    typeof(DependencyExtension).Assembly.GetName().Name!
                ),
            ServiceLifetime.Scoped
        );
        services.AddScoped<IDbConnection>(sp => new SqlConnection(
            configuration.GetConnectionString("EcommerceDbContext")
        ));
        services.AddHttpContextAccessor();
        // services.AddLogging();
        // services.AddSingleton<ILoggerFactory, LoggerFactory>();
        // services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
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
            .AddScoped<ISeederRepository, SeederRepository>();

        return services;
    }
}
