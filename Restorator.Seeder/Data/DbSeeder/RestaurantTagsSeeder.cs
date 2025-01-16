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
                new() { Name = "Десерты" },
                new() { Name = "Итальянская кухня" },
                new() { Name = "Русская кухня" },
                new() { Name = "Грузинская кухня" },
                new() { Name = "Татарская кухня" },
                new() { Name = "Японская кухня" }, 
                new() { Name = "Кофейня" }, 
                new() { Name = "Востояная кухня" },
                new() { Name = "Вьетнамская кухня" },
                new() { Name = "Пекарня" },
            };

            _context.RestaurantTags.AddRange(tags);

            await _context.SaveChangesAsync();
        }
    }
}
