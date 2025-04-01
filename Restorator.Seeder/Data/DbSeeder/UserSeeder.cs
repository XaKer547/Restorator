using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.DataAccess.Extensions;
using Restorator.DataAccess.Helpers;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedUsersAsync()
        {
            if (_context.Users.Any())
                return;

            var users = new List<User>()
            {
                new()
                {
                    Role = _context.Roles.FromEnum(Roles.User),
                    Username = "Шелкопряд Тутовый",
                    Login = "Silk",
                    Verified = true,
                    Password = AccountPasswordHelper.HashUserPassword("MasterPassword")
                },
                new()
                {
                    Role = _context.Roles.FromEnum(Roles.Admin),
                    Username = "Cool adm",
                    Login = "admin",
                    Verified = true,
                    Password = AccountPasswordHelper.HashUserPassword("admin")
                },
                new()
                {
                    Role = _context.Roles.FromEnum(Roles.Manager),
                    Username = "Манагер",
                    Login = "Manager",
                    Verified = true,
                    Password = AccountPasswordHelper.HashUserPassword("Manager")
                }
            };

            _context.Users.AddRange(users);

            await _context.SaveChangesAsync();
        }
    }
}