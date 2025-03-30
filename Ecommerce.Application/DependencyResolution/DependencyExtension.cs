namespace Ecommerce.Application.DependencyResolution;

public static class DependencyExtension
{
    public static IServiceCollection AddEcommerceApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyExtension).Assembly;
        // services.AddMediatR(_ => _.RegisterServicesFromAssemblies(assembly));
        services.AddMediatR(cfg =>
            // cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly)
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()) //try
        );
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
