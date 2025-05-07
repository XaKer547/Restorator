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
                 opt.UseSqlServer("Server=b2-225-002\\SQLEXPRESS;Database=Restorator;TrustServerCertificate=true;Trusted_connection=true");
             });

            return services;
        }
    }
}
