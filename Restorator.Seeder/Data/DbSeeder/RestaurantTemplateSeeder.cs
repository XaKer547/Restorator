using Restorator.DataAccess.Data.Entities;

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
                new() {
                    Image  = "20 мест(1).png",
                    Tables =
                    [
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
                    ],
                }, // 20 мест (1)
                new()
                {
                    Image  = "30 мест(1).png",
                    Tables =
                    [
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
                    ],
                }, // 30 мест (1)
                new()
                {
                    Image  = "40 мест(1).png",
                    Tables =
                    [
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
                    ],
                }, // 40 мест (1)
                new()
                {
                    Image  = "50 мест(1).png",
                    Tables =
                    [
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
                    ],
                }, // 50 мест (1)
                new()
                {
                    Image  = "100 мест(1).png",
                    Tables =
                    [
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
                    ],
                }, // 100 мест (1)
                new()
                {
                    Image  = "100+ мест(1).png",
                    Tables =
                    [
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
                    ],
                }, // 100+ мест (1)
                new()
                {
                Image  = "100 мест(2).png",
                Tables =
                    [
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
                    ]
            }, // 100 мест (2)
                new()
                {
                Image  = "100 мест(3).png",
                Tables =
                    [
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
                    ]
            }, // 100 мест (3)
                new()
                {
                    Image  = "100 мест(4).png",
                    Tables =
                        [
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
                        ]
                }, // 100 мест (4)
                new()
                {
                    Image  = "100 мест(5).png",
                    Tables =
                        [
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
                        ]
                }, // 100 мест (5)
                new()
                {
                    Image  = "100+ мест(2).png",
                    Tables =
                        [
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
                        ]
                }, // 100+ мест (2)
                new()
                {
                    Image  = "100+ мест(3).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 130.51F,
                                Y = 170F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 830.11F,
                                Y = 163.58F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 60.21F,
                                Y = 729.47F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 368.17F,
                                Y = 725.05F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 746.42F,
                                Y = 725.05F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1056.61F,
                                Y = 725.05F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 1010.86F,
                                Y = 1012.42F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 1017.56F,
                                Y = 1299.79F,
                            },
                        ]
                }, // 100+ мест (3)
                new()
                {
                    Image  = "100+ мест(4).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 58.76F,
                                Y = 124.19F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 58.76F,
                                Y = 405.96F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 522.99F,
                                Y = 125.99F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 809.17F,
                                Y = 145.59F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 821.88F,
                                Y = 1118.1F,
                            },
                        ]
                }, // 100+ мест (4)
                new()
                {
                    Image  = "100+ мест(5).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 107.51F,
                                Y = 16.8F,
                            },
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 107.51F,
                                Y = 296.77F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 412.76F,
                                Y = 47.6F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 724.37F,
                                Y = 47.6F,
                            },
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 999.95F,
                                Y = 16.8F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 18.48F,
                                Y = 1038.7F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 16.36F,
                                Y = 1307.48F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 787.97F,
                                Y = 540.35F,
                            },
                        ]
                }, // 100+ мест (5)
                new()
                {
                    Image  = "20 мест(2).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 40.129999999999995F,
                                Y = 515.26F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 36.78F,
                                Y = 888.63F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 476.28F,
                                Y = 269.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1061.07F,
                                Y = 313.89F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1061.07F,
                                Y = 649.89F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 103.73F,
                                Y = 1330.74F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1064.42F,
                                Y = 954.95F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 1000.82F,
                                Y = 1330.74F,
                            },
                        ]
                }, // 20 мест (2)
                new()
                {
                    Image  = "20 мест(3).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 20.97F,
                                Y = 110.16F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 517.04F,
                                Y = 95.67F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 1010.9F,
                                Y = 104.36F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 56.09F,
                                Y = 379.77F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 38.53F,
                                Y = 1037.85F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 293.15F,
                                Y = 1377.03F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 589.47F,
                                Y = 1379.93F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 877.01F,
                                Y = 1379.93F,
                            },
                        ]
                }, // 20 мест (3)
                new()
                {
                    Image  = "20 мест(4).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 24.39F,
                                Y = 17.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 4,
                                X = 13.35F,
                                Y = 477.47F,
                            },
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 528.84F,
                                Y = 521.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 50.17F,
                                Y = 751.58F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1066.65F,
                                Y = 849.84F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1066.65F,
                                Y = 560F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1070F,
                                Y = 17.68F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1070F,
                                Y = 1400F,
                            },
                        ]
                }, // 20 мест (4)
                new()
                {
                    Image  = "20 мест(5).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 106.58F,
                                Y = 14.5F,
                            },
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 997.73F,
                                Y = 14.5F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 36.34F,
                                Y = 513.12F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 38.53F,
                                Y = 965.37F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 115.36F,
                                Y = 1361.64F,
                            },
                            new Table()
                            {
                                TableTemplateId = 6,
                                X = 554.35F,
                                Y = 507.33F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1065.39F,
                                Y = 527.62F,
                            },
                        ]
                }, // 20 мест (5)
                new()
                {
                    Image  = "30 мест(2).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 60.48F,
                                Y = 52.18F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 60.48F,
                                Y = 890F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 60.48F,
                                Y = 1222.48F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 350.22F,
                                Y = 530.52F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 670.68F,
                                Y = 1069.74F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 490.7F,
                                Y = 49.28F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 446.8F,
                                Y = 1005.96F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 442.41F,
                                Y = 1298.76F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1067.81F,
                                Y = 1008.86F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1065.58F,
                                Y = 1301.66F,
                            },
                        ]
                }, // 30 мест (2)
                new()
                {
                    Image  = "30 мест(3).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 301.22F,
                                Y = 53.05F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 548.93F,
                                Y = 300.63F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 100.38F,
                                Y = 1153.89F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 475.28F,
                                Y = 956.95F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 702.91F,
                                Y = 641.05F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1070F,
                                Y = 132.63F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1070F,
                                Y = 424.42F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1067.77F,
                                Y = 720.63F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1070F,
                                Y = 1003.58F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 1067.77F,
                                Y = 1299.79F,
                            },
                        ]
                }, // 30 мест (3)
                new()
                {
                    Image  = "30 мест(4).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 45.12F,
                                Y = 133.35F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 47.31F,
                                Y = 426.15F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 383.14F,
                                Y = 126.66F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 376.56F,
                                Y = 481.24F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 828.72F,
                                Y = 130.46F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 604.84F,
                                Y = 808.82F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 822.14F,
                                Y = 481.24F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 841.89F,
                                Y = 1385.73F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 589.47F,
                                Y = 1400F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 1070F,
                                Y = 1398.32F,
                            },
                        ]
                }, // 30 мест (4)
                new()
                {
                    Image  = "30 мест(5).png",
                    Tables =
                        [
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 75.85F,
                                Y = 52.18F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 196.57F,
                                Y = 376.87F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 198.77F,
                                Y = 884.2F,
                            },
                            new Table()
                            {
                                TableTemplateId = 2,
                                X = 47.31F,
                                Y = 1232.08F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 686.05F,
                                Y = 46.38F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 980.17F,
                                Y = 43.49F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 642.15F,
                                Y = 379.77F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 648.73F,
                                Y = 756.64F,
                            },
                            new Table()
                            {
                                TableTemplateId = 9,
                                X = 646.54F,
                                Y = 1098.73F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 683.85F,
                                Y = 1380.93F,
                            },
                            new Table()
                            {
                                TableTemplateId = 7,
                                X = 977.98F,
                                Y = 1380.93F,
                            },
                        ]
                }, // 30 мест (5)
            };

            _context.RestaurantTemplates.AddRange(templates);

            await _context.SaveChangesAsync();
        }
    }
}