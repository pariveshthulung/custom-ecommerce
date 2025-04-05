using Ecommerce.Shared.Application.Behaviours;
using FluentValidation.AspNetCore;
using static Ecommerce.Application.Features.AuthFeature.Commands.LoginCommand;

namespace Ecommerce.Application.DependencyResolution;

public static class DependencyExtension
{
    public static IServiceCollection AddEcommerceApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyExtension).Assembly;
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); //try
            // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        services.AddAutoMapper(assembly);
        // services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
