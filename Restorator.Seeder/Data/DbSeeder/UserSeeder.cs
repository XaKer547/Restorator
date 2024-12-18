using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Helpers;

namespace Restorator.Seeder.Data.DbSeeder
{
    public partial class RestoratorDbSeeder
    {
        private async Task SeedUsersAsync()
        {
            var users = new List<User>()
            {
                new()
                {
                    RoleId = 1,
                    Username = "Шелкопряд Тутовый",
                    Account = new Account()
                    {
                        Login = "Silk",
                        Password = AccountPasswordHelper.HashUserPassword("MasterPassword")
                    },
                },
                new()
                {
                    RoleId = 2,
                    Username = "Cool adm",
                    Account = new Account()
                    {
                        Login = "admin",
                        Password = AccountPasswordHelper.HashUserPassword("admin")
                    }
                }
            };

            _context.Users.AddRange(users);

            await _context.SaveChangesAsync();
        }
    }
}