using System.Reflection;

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
        //add validation
        services.AddValidatorsFromAssembly(assembly);
        // services.AddLogging();
        // services.AddSingleton<ILoggerFactory, LoggerFactory>();
        // services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        return services;
    }
}
