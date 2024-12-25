using Restorator.DataAccess.Data.Entities;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedRestaurantTagsAsync()
        {
            if (_context.RestaurantTags.Any())
                return;

            var tags = new List<RestaurantTag>()
            {
                new ()
                {
                    Name = "Десерты"
                },
                new()
                {
                    Name = "Шаурма"
                },
                new()
                {
                Name = "Лаунж"
                }
            };

            _context.RestaurantTags.AddRange(tags);

            await _context.SaveChangesAsync();
        }
    }
}
