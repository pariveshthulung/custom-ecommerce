using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.DependencyResolution;

public static class DependencyExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyExtension).Assembly;
        services.AddMediatR(_ => _.RegisterServicesFromAssemblies(assembly));
        services.AddAutoMapper(assembly);
        //add validation 
        return services;
    }
}
