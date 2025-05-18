using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restorator.DataAccess.Data;
using Restorator.Seeder.Data.DbSeeder;

namespace Restorator.Seeder.HostedServices
{
    public class DbSeederService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DbSeederService> _logger;
        public DbSeederService(IServiceProvider serviceProvider,
                               ILogger<DbSeederService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<RestoratorDbContext>();

            try
            {
                var pending = await context.Database.GetPendingMigrationsAsync(stoppingToken);

                if (!pending.Any()) //maybe refactor
                {
                    _logger.LogInformation("No seeding magic");

                    return;
                }

                await context.Database.MigrateAsync(stoppingToken);

                var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

                await seeder.SeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during running Seeder worker");
            }
        }
    }
}