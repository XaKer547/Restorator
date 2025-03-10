using Restorator.DataAccess.SqlServer;
using Restorator.Seeder.Data.DbSeeder;
using Restorator.Seeder.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRestoratorDbContext();

builder.Services.AddScoped<IDbSeeder, RestoratorDbSeeder>();

builder.Services.AddHostedService<DbSeederService>();

var app = builder.Build();

app.Run();