using Restorator.DataAccess.Data.Entities;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedTableTemplatesAsync()
        {
            if (_context.TableTemplates.Any())
                return;

            var templates = new List<TableTemplate>()
            {
                new TableTemplate() { Height = 183, Width = 183 },
                new TableTemplate()
                {
                    Height = 183,
                    Width = 183,
                    Rotation = 45,
                },
                new TableTemplate()
                {
                    Height = 183,
                    Width = 183,
                    Rotation = 90,
                },

                new TableTemplate() { Height = 110, Width = 183 },
                new TableTemplate()
                {
                    Height = 110,
                    Width = 183,
                    Rotation = 45,
                },
                new TableTemplate()
                {
                    Height = 110,
                    Width = 183,
                    Rotation = 90,
                },

                new TableTemplate() { Height = 108, Width = 100 },
                new TableTemplate()
                {
                    Height = 108,
                    Width = 100,
                    Rotation = 45,
                },
                new TableTemplate()
                {
                    Height = 108,
                    Width = 100,
                    Rotation = 90,
                },
            };

            _context.TableTemplates.AddRange(templates);

            await _context.SaveChangesAsync();
        }
    }
}
