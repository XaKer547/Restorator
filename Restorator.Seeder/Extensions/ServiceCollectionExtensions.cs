using Microsoft.Extensions.DependencyInjection;
using Restorator.Seeder.Data.DbSeeder;
using Restorator.Seeder.HostedServices;

namespace Restorator.Seeder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeeder(this IServiceCollection services)
        {
            services.AddScoped<IDbSeeder, RestoratorDbSeeder>();
            services.AddHostedService<DbSeederService>();

            return services;
        }
    }
}
