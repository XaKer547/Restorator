using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restorator.DataAccess.Data;

namespace Restorator.DataAccess.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestoratorDbContext(this IServiceCollection services)
        {


            return services;
        }
    }
}
