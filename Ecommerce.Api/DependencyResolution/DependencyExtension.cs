using System.Reflection;
using Ecommerce.Domain.AggregatesModel.AppUserAggregate.Entities;
using Ecommerce.Domain.Enumerations;
using Ecommerce.Infrastructure.Data;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

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

        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version")
                );
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        services
            .AddIdentity<AppUser, IdentityRole<long>>()
            .AddEntityFrameworkStores<EcommerceDbContext>()
            .AddDefaultTokenProviders();

        var assembly = typeof(DependencyExtension).Assembly;
        // services.AddMediatR(_ => _.RegisterServicesFromAssemblies(assembly));
        services.AddMediatR(cfg =>
            // cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly)
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()) //try
        );
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);
        // services.AddLogging();
        // services.AddSingleton<ILoggerFactory, LoggerFactory>();
        // services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        return services;
    }
}
