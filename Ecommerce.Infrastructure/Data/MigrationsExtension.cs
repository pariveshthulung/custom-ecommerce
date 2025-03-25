using Ecommerce.Shared.Extension;

namespace Ecommerce.Infrastructure.Data;

public static class MigrationsExtension
{
    public static IHost UseEcommerceMigration(this IHost app)
    {
        //configure seed
        app.MigrateDbContext<EcommerceDbContext>(
            (context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var settings = services.GetService<IOptions<EcommerceInfrastructureSettings>>();
                var logger = services.GetService<ILogger<EcommerceDbContextSeeder>>();

                if (settings?.Value?.EnableMigration ?? false)
                    context.Database.Migrate();

                new EcommerceDbContextSeeder().SeedAsync(context, env, settings, logger).Wait();
            }
        );

        return app;
    }
}
