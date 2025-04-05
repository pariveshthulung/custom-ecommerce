using Ecommerce.Shared.Application.Behaviours;
using FluentValidation.AspNetCore;

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
        services.AddQuartz(); // for background services

        // services
        //     .AddApiVersioning(options =>
        //     {
        //         options.DefaultApiVersion = new ApiVersion(1.0);
        //         options.ReportApiVersions = true;
        //         options.AssumeDefaultVersionWhenUnspecified = true;
        //         options.ApiVersionReader = ApiVersionReader.Combine(
        //             new UrlSegmentApiVersionReader(),
        //             new HeaderApiVersionReader("X-Api-Version")
        //         );
        //     })
        //     // .AddMvc()
        //     .AddApiExplorer(options =>
        //     {
        //         options.GroupNameFormat = "'v'V";
        //         options.SubstituteApiVersionInUrl = true;
        //     });
        services.AddScoped<PasswordHasher<AppUser>>();
        services
            .AddIdentity<AppUser, IdentityRole<long>>()
            .AddEntityFrameworkStores<EcommerceDbContext>()
            .AddDefaultTokenProviders();
        var assembly = typeof(DependencyExtension).Assembly;
        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()) //try
        );
        services.AddAutoMapper(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
