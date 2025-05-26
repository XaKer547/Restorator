using Restorator.DataAccess.Data.Entities;

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
                    Description =
                        "Погрузитесь в мир сладких грез и пряных ароматов в “Синабонной Бо Синна”, где каждый ролл создан с душой и любовью, вдохновлённый самим Бо Синном! Здесь каждое блюдо — это произведение кулинарного искусства, приготовленное с заботой о ваших вкусовых рецепторах. Мы специализируемся на классических и авторских синабонах, приготовленных из нежнейшего теста, щедро сдобренных ароматной корицей и сливочным кремом, в точности как это делал сам маэстро.",
                    TemplateId = 1,
                    Images  = [new RestaurantImage { Image = "Синабонная Бо Синна.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 1)],
                    OwnerId = 3,
                },
                new()
                {
                    Name = "GIACOMO",
                    Description =
                        "GIACOMO — современная итальянская кухня от Kravchenko Group\r\n\r\nРимская пицца на хрустящем тесте, свежая паста ручной работы, открытая кухня, завораживающие подачи, десерты от шеф-кондитера, авторские коктейли, винная карта, которая никого не оставит равнодушным, стильный интерьер и современная итальянская музыка в самом сердце города!",
                    TemplateId = 2,
                    Images  = [new RestaurantImage { Image = "GIACOMO(1).png" }, new RestaurantImage { Image = "GIACOMO(2).png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 2)],
                    OwnerId = 3,
                },
                new()
                {
                    Name = "Гопак",
                    Description =
                        "Ресторан «Гопак» - это атмосферное\r\nзаведение с просторным и уютным залом,\r\nоформленным в русском народном стиле.\r\nГостям нравится местная кухня, особенно\r\nборщ, пампушки, сало и кисель, а также\r\nмясные блюда, такие как стейк из свинины,\r\nпельмени и вареники. Кроме того,\r\nпосетители отмечают, что персонал\r\nресторана очень дружелюбный и\r\nвнимательный.",
                    TemplateId = 3,
                    //Images  = ["Гопак.png"],
                    //MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 3)],
                    OwnerId = 3,
                },
                new()
                {
                    Name = "Антресоль",
                    Description =
                        "Наш гостеприимный грузинский дом «Антресоль», от всей души приветствуем вас. У нас всегда уютно, тепло, весело, и каждый может почувствовать себя дорогим гостем! Мы не только сытно накормим или организуем шумное застолье, но и научим васхитительно готовить в нашей кулинарной школе. \r\n \r\nСкорее бронируйте столик и заказывайте сочные хачапури, ароматные хинкали, неповторимые блюда открытого огня и другие шедевры настоящей грузинской кухни!",
                    TemplateId = 4,
                    Images  = [new RestaurantImage { Image = "Антресоль.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 4)],
                    OwnerId = 3,
                },
                new()
                {
                    Name = "Алазани",
                    Description =
                        "Алазани - ресторан современной грузинской кухни, со своими старинными тбилисскими секретами, входит в ресторанный холдинг «MATRESHKI GROUP»",
                    TemplateId = 5,
                    //Images  = ["Алазани.png"],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 4)],
                    OwnerId = 3,
                },
                 new()
                 {
                    Name = "Телави",
                    Description =
                        "Ресторан «Телави» предлагает своим гостям блюда грузинской кухни, а также широкий выбор напитков, включая авторские чаи и лимонады.",
                    TemplateId = 7,
                    Images  = [new RestaurantImage { Image = "Телави.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 4)],
                    OwnerId = 3,
                 },
                 new()
                 {
                    Name = "ТатарАш",
                    Description =
                        "Ресторан «ТатарАш» предлагает своим гостям блюда татарской кухни.",
                    TemplateId = 10,
                    Images  = [new RestaurantImage { Image = "ТатарАш.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 5)],
                    OwnerId = 3,
                 },
                 new()
                 {
                    Name = "Yoko Sushi Bar",
                    Description =
                        "Суши-бар «Yoko Sushi Bar» предлагает своим гостям блюда итальянской и японской кухни, а также широкий выбор напитков, включая авторские чаи.",
                    TemplateId = 13,
                    Images  = [new RestaurantImage { Image = "Yoko Sushi Bar.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 6 || t.Id == 2)], //итальянская и японская
                    OwnerId = 3,
                 },
                 new()
                 {
                    Name = "Kinza",
                    Description =
                        "Ресторан «Kinza» предлагает своим гостям изысканные блюда, такие как хачапури, хинкали, салаты, паста и пицца, а также фирменные напитки, такие как чай с брусникой и специями.",
                    TemplateId = 20,
                    Images  = [new RestaurantImage { Image = "Kinza.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 8)],
                    OwnerId = 3,
                 },
                 new()
                 {
                    Name = "Suly",
                    Description =
                        "Ресторан «Suly» предлагает своим гостям блюда вьетнамской кухни, приготовленные по традиционным рецептам. В меню представлены такие блюда, как фо-бо, том ям, лапша с различными соусами, креветки в кляре и многие другие.",
                    TemplateId = 15,
                    Images  = [new RestaurantImage { Image = "Suly.png" }],
                    MenuImage = "Menu.png",
                    BeginWorkTime = new TimeOnly(12, 0),
                    EndWorkTime = new TimeOnly(0, 0),
                    Approved = true,
                    Tags = [.. _context.RestaurantTags.Where(t => t.Id == 9)],
                    OwnerId = 3,
                 },
            };

            _context.Restaurants.AddRange(restaurants);

            await _context.SaveChangesAsync();
        }
    }
}
