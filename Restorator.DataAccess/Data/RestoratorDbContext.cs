using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data.Entities;

namespace Restorator.DataAccess.Data
{
    public class RestoratorDbContext : DbContext
    {
        public RestoratorDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<RestaurantTemplate> RestaurantTemplates => Set<RestaurantTemplate>();
        public DbSet<Table> Tables => Set<Table>();
        public DbSet<TableTemplate> TableTemplates => Set<TableTemplate>();
    }
}