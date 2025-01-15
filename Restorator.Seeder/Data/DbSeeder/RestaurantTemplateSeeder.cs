using Restorator.DataAccess.Data.Entities;
using Restorator.Seeder.Helpers;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedRestaurantTemplatesAsync()
        {
            if (_context.RestaurantTemplates.Any())
                return;

            var templates = new List<RestaurantTemplate>()
            {
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("20 мест(1)"),
                    Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 357.86F,
                            Y = 39.48F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 37.35F,
                            Y = 529.47F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 366.2F,
                            Y = 591.84F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 667.57F,
                            Y = 943.24F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 34.82F,
                            Y = 891.02F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 320.03F,
                            Y = 1382.75F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 1063.2F,
                            Y = 939.46F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 683.51F,
                            Y = 1386.1F,
                        },
                    },
                }, // 20 мест (1)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("30 мест(1)"),
                    Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 483.14F,
                            Y = 5.9F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 105.19F,
                            Y = 400.19F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 105.31F,
                            Y = 1238.47F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 921.38F,
                            Y = 465.69F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 513.01F,
                            Y = 611.71F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 107.25F,
                            Y = 824.53F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 513.02F,
                            Y = 1233.46F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 909.82F,
                            Y = 1237.21F,
                        },
                    },
                }, // 30 мест (1)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("40 мест(1)"),
                    Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 18F,
                            Y = 781.35F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 17.46F,
                            Y = 520.41F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 18.130000000000003F,
                            Y = 1311.94F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 1007.18F,
                            Y = 519.03F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 514.9100000000001F,
                            Y = 694.21F,
                        },
                        new Table()
                        {
                            TableTemplateId = 1,
                            X = 514.3199999999999F,
                            Y = 1033.96F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 1007.05F,
                            Y = 1313.21F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 17.509999999999998F,
                            Y = 1048.38F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 1007.7F,
                            Y = 783.71F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 1007.49F,
                            Y = 1047.73F,
                        },
                    },
                }, // 40 мест (1)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("50 мест(1)"),
                    Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 53.21F,
                            Y = 719.35F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 360.64F,
                            Y = 439.24F,
                        },
                        new Table()
                        {
                            TableTemplateId = 9,
                            X = 685.44F,
                            Y = 1373.42F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 759.74F,
                            Y = 441.38F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 360.68F,
                            Y = 761.47F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 955.37F,
                            Y = 59.87F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 978.55F,
                            Y = 1328.25F,
                        },
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 53.26F,
                            Y = 422.19F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 760.86F,
                            Y = 759.9F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 981.35F,
                            Y = 1040.77F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 547.52F,
                            Y = 58.86F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 150.05F,
                            Y = 58.08F,
                        },
                    },
                }, // 50 мест (1)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("100 мест(1)"),
                    Tables = new List<Table>()
                    {
                        new Table() // 39
                        {
                            TableTemplateId = 4,
                            X = 18.21F,
                            Y = 781.93F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 18.39F,
                            Y = 1046.88F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 18.97F,
                            Y = 1311.02F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 409,
                            Y = 1031.3F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 409,
                            Y = 1325.2F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 665,
                            Y = 1030.87F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 980,
                            Y = 1049.17F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 980,
                            Y = 1312.25F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 665,
                            Y = 1325.79F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 733.03F,
                            Y = 137.05F,
                        },
                    },
                }, // 100 мест (1)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("100+ мест(1)"),
                    Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 109.55F,
                            Y = 22.26F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 999.42F,
                            Y = 20.28F,
                        },
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 414.03F,
                            Y = 53.34F,
                        },
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 722.49F,
                            Y = 53.34F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 730.87F,
                            Y = 1003.13F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 736.16F,
                            Y = 462.41F,
                        },
                    },
                }, // 100+ мест (1)
            };

            _context.RestaurantTemplates.AddRange(templates);

            await _context.SaveChangesAsync();
        }
    }
}