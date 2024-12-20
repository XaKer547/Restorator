using Restorator.DataAccess.Data.Entities;
using Restorator.Seeder.Helpers;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedRestaurantTemplatesAsync()
        {
            var templates = new List<RestaurantTemplate>()
            {
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetByteArrayFromResource("20 мест(1).png"),
                    Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 366.09F,
                            Y = 592.63F,
                        }
                    }




                }
            };
        }
    }
}
