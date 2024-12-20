using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restorator.DataAccess.Data;
using Restorator.Seeder.Helpers;

internal class Program
{
    private readonly RestoratorDbContext _context = _provider.GetRequiredService<RestoratorDbContext>();
    private static void Main()
    {
        var a = EmbeddedResourceHelper.GetByteArrayFromResource("20 мест(1).png");
    }

    private static readonly IServiceProvider _provider = new ServiceCollection()
        .AddDbContext<RestoratorDbContext>(opt =>
        {
            opt.UseSqlServer("Server=b2-225-002\\SQLEXPRESS;Database=Restorator;TrustServerCertificate=true;Trusted_connection=true");
        })
        .BuildServiceProvider();
}