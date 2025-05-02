using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restorator.DataAccess.Data;

namespace Restorator.DataAccess.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestoratorDbContext(this IServiceCollection services)
        {
            services.AddDbContext<RestoratorDbContext>(opt =>
             {
                 opt.UseSqlServer("Server=DESKTOP-F1TRK20\\SQLEXPRESS;Database=Restorator;TrustServerCertificate=true;Trusted_connection=true");
             });

            return services;
        }
    }
}
