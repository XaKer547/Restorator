using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder : IDbSeeder
    {
        private readonly RestoratorDbContext _context;
        public RestoratorDbSeeder(RestoratorDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();

            await SeedUsersAsync();

            await SeedTableTemplatesAsync();

            await SeedRestaurantTagsAsync();

            await SeedRestaurantTemplatesAsync();

            await SeedRestaurantsAsync();
        }
    }

    public interface IDbSeeder
    {
        Task SeedAsync();
    }
}
