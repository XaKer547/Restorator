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
                    Email = "noreply@silk.ru",
                    Login = "Silk",
                    Verified = true,
                    Password = AccountPasswordHelper.HashUserPassword("MasterPassword")
                },
                new()
                {
                    Role = _context.Roles.FromEnum(Roles.Admin),
                    Username = "Cool adm",
                    Email = "noreply@restorator.ru",
                    Login = "admin",
                    Verified = true,
                    Password = AccountPasswordHelper.HashUserPassword("admin")
                },
                new()
                {
                    Role = _context.Roles.FromEnum(Roles.Manager),
                    Username = "Манагер",
                    Email = "noreply@BoSin.com",
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