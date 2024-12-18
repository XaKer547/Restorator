using Restorator.DataAccess.Data.Entities;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedTableTemplatesAsync()
        {
            var templates = new List<TableTemplate>()
            {
                new TableTemplate()
                {
                    Rotation = 90,

                }
            };

            _context.TableTemplates.AddRange(templates);

            await _context.SaveChangesAsync();
        }
    }
}
