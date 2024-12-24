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
                new TableTemplate()
                {
                    Height = 183,
                    Width = 183,
                }
            };

            _context.TableTemplates.AddRange(templates);

            await _context.SaveChangesAsync();
        }
    }
}
