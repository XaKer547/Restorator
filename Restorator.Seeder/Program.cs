using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.Seeder.Data.DbSeeder;
using Restorator.Seeder.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestoratorDbContext>(opt =>
{
#if HOMEDEBUG 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Home"));
#elif COLLEGEDEBUG || DEBUG
    opt.UseSqlServer(builder.Configuration.GetConnectionString("College"));
#endif
});

builder.Services.AddScoped<IDbSeeder, RestoratorDbSeeder>();

builder.Services.AddHostedService<DbSeederService>();

var app = builder.Build();

app.Run();