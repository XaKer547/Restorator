using Restorator.Seeder.Data.DbSeeder;

namespace Restorator.Seeder.HostedServices
{
    public class DbSeederService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public DbSeederService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

            await seeder.SeedAsync();
        }
    }
}
