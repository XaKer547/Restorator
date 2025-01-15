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
                new RestaurantTemplate
                {
                Image = EmbeddedResourceHelper.GetRestaurantPlan("100 мест(2)"),
                Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 103.73F,
                            Y = 39.79F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 163.98F,
                            Y = 336F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 163.98F,
                            Y = 601.26F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 163.98F,
                            Y = 857.68F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 933.87F,
                            Y = 1030.11F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 930.53F,
                            Y = 773.68F,
                        },
                        new Table()
                        {
                            TableTemplateId = 6,
                            X = 930.53F,
                            Y = 508.42F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 86.99F,
                            Y = 1158.32F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 866.93F,
                            Y = 1246.74F,
                        },
                    }
            }, // 100 мест (2)
                new RestaurantTemplate
                {
                Image = EmbeddedResourceHelper.GetRestaurantPlan("100 мест(3)"),
                Tables = new List<Table>()
                    {
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 415.03F,
                            Y = 1083.16F,
                        },
                        new Table()
                        {
                            TableTemplateId = 7,
                            X = 415.03F,
                            Y = 1321.89F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 20.04F,
                            Y = 1047.79F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 20.04F,
                            Y = 1310.63F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 1014.21F,
                            Y = 632.21F,
                        },
                        new Table()
                        {
                            TableTemplateId = 4,
                            X = 1014.21F,
                            Y = 897.47F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 97.03F,
                            Y = 128.21F,
                        },
                        new Table()
                        {
                            TableTemplateId = 2,
                            X = 813.37F,
                            Y = 128.21F,
                        },
                    }
            }, // 100 мест (3)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("100 мест(4)"),
                    Tables = new List<Table>()
                        {
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 107.07F,
                                Y = 22.11F,
                            },
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 374.17F,
                                Y = 22.11F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 110.42F,
                                Y = 296.21F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 986.43F,
                                Y = 384.63F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 799.98F,
                                Y = 66.32F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 418.38F,
                                Y = 1388.21F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 125.81F,
                                Y = 1131.79F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 123.81F,
                                Y = 1388.21F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 984.08F,
                                Y = 982.47F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 823.41F,
                                Y = 1224.63F,
                            },
                        }
                }, // 100 мест (4)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("100 мест(5)"),
                    Tables = new List<Table>()
                        {
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 20.04F,
                                Y = 101.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 20.04F,
                                Y = 366.95F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 20.04F,
                                Y = 636.63F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 773.2F,
                                Y = 1109.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 773.2F,
                                Y = 1366.11F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 1015.21F,
                                Y = 1109.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 1014.21F,
                                Y = 1366.11F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 806.67F,
                                Y = 128.21F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 820.06F,
                                Y = 675F,
                            },
                        }
                }, // 100 мест (5)
                new RestaurantTemplate
                {
                    Image = EmbeddedResourceHelper.GetRestaurantPlan("100+ мест(2)"),
                    Tables = new List<Table>()
                        {
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 739.21F,
                                Y = 277.17F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 834.6F,
                                Y = 1120.9F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 122.35F,
                                Y = 1119.1F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 29.08F,
                                Y = 744.73F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 270.74F,
                                Y = 744.73F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 1010.55F,
                                Y = 705.53F,
                            },
                        }
                }, // 100+ мест (2)




            };

            _context.RestaurantTemplates.AddRange(templates);

            await _context.SaveChangesAsync();
        }
    }
}