using Restorator.DataAccess.Data.Entities.Enums;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any())
                return;

            Enum.GetValues<Roles>()
                .ToList()
                .ForEach(instance => _context.Roles.Add(instance));

            await _context.SaveChangesAsync();
        }
    }
}