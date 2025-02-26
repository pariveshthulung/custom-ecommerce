namespace Ecommerce.Api.DependencyResolution;

public static class DependencyExtension
{
    public static IServiceCollection AddEcommerce(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration
    )
    {
        services.AddControllersWithViews();
        services.AddEcommerceApplication();
        services.AddEcommerceInfrastructure(environment, configuration);

        // var assembly = typeof(DependencyExtension).Assembly;
        // services.AddMediatR(_ => _.RegisterServicesFromAssemblies(assembly));
        // services.AddAutoMapper(assembly);

        return services;
    }
}
