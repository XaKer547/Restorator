using Restorator.DataAccess.Data.Entities;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any())
                return;

            var roles = new List<Role>()
            {
                new()
                {
                    Name = "Клиент",
                },
                new()
                {
                    Name = "Администратор"
                }
            };

            _context.Roles.AddRange(roles);

            await _context.SaveChangesAsync();
        }
    }
}