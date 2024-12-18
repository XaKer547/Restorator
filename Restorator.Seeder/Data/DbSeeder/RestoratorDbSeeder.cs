using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private readonly RestoratorDbContext _context;
        public RestoratorDbSeeder(RestoratorDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await SeedRolesAsync();

            await SeedUsersAsync();

            await SeedTableShapesAsync();

            await SeedTableTemplatesAsync();

            await SeedRestaurantTemplatesAsync();

            await SeedRestaurantsAsync();
        }
    }

    public partial class RestoratorDbSeeder
    {
        private async Task SeedRestaurantsAsync()
        {

        }
    }


    public partial class RestoratorDbSeeder
    {
        private async Task SeedReservationsAsync()
        {

        }


    }


    public partial class RestoratorDbSeeder
    {
        private async Task SeedAccountAsync()
        {

        }
    }
}
