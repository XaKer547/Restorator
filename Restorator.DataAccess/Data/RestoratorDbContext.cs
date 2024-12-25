using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data.Entities;

namespace Restorator.DataAccess.Data
{
    public class RestoratorDbContext : DbContext
    {
        public RestoratorDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<RestaurantTag> RestaurantTags => Set<RestaurantTag>();
        public DbSet<RestaurantTemplate> RestaurantTemplates => Set<RestaurantTemplate>();
        public DbSet<Table> Tables => Set<Table>();
        public DbSet<TableTemplate> TableTemplates => Set<TableTemplate>();

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Restaurant>()
        //        .Property(r => r.BeginWorkTime)
        //        .HasConversion(r => r.ToTimeSpan(), r => TimeOnly.FromTimeSpan(r));

        //    modelBuilder.Entity<Restaurant>()
        //        .Property(r => r.EndWorkTime)
        //        .HasConversion(r => r.ToTimeSpan(), r => TimeOnly.FromTimeSpan(r));

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}