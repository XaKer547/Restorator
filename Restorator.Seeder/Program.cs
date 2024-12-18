using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;

internal class Program
{
    private readonly RestoratorDbContext _context = _provider.GetRequiredService<RestoratorDbContext>();
    private static void Main()
    {
        var restaurantTemplate = new RestaurantTemplate()
        {
            Image = File.ReadAllBytes(@"C:\Users\a.panfilov\Desktop\templates\20 мест(1).png")
        };

    }


    private static readonly IServiceProvider _provider = new ServiceCollection()
        .AddDbContext<RestoratorDbContext>(opt =>
        {
            opt.UseSqlServer("Server=b2-225-002\\SQLEXPRESS;Database=Restorator;TrustServerCertificate=true;Trusted_connection=true");
        })
        .BuildServiceProvider();
}