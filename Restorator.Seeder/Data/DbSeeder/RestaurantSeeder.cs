using Restorator.DataAccess.Data.Entities;
using Restorator.Seeder.Helpers;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedRestaurantsAsync()
        {
            if (_context.Restaurants.Any())
                return;

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    Name = "Синабонная Бо Синна",
                    Description = "Погрузитесь в мир сладких грез и пряных ароматов в “Синабонной Бо Синна”, где каждый ролл создан с душой и любовью, вдохновлённый самим Бо Синном! Здесь каждое блюдо — это произведение кулинарного искусства, приготовленное с заботой о ваших вкусовых рецепторах. Мы специализируемся на классических и авторских синабонах, приготовленных из нежнейшего теста, щедро сдобренных ароматной корицей и сливочным кремом, в точности как это делал сам маэстро.",
                    TemplateId = 1,
                    Image = EmbeddedResourceHelper.GetRestaurantImage("Синабонная Бо Синна"),
                    MenuImage = EmbeddedResourceHelper.GetRestaurantMenu("Синабонная Бо Синна"),
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 1)],
                    OwnerId = 3
                },
            };

            _context.Restaurants.AddRange(restaurants);

            await _context.SaveChangesAsync();
        }
    }
}
