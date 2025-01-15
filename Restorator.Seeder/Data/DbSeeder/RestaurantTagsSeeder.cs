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
                new() { Name = "Японская кухня" }, // делай раздельно каждый тэг, ибо у ресторана может быть их множество
                new() { Name = "Кофейня" }, // от слова Кофе, бро... тебе определенно стоит в ней побывать, а то ночные поседелки к добру не приведут)
                new() { Name = "Востояная кухня" },
                new() { Name = "Вьетнамская кухня" },
                new() { Name = "Пекарня" },
            };

            _context.RestaurantTags.AddRange(tags);

            await _context.SaveChangesAsync();
        }
    }
}
