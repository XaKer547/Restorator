using Restorator.DataAccess.Data;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestoratorDbContext _context;
        public RestaurantService(RestoratorDbContext context)
        {
            _context = context;
        }
    }
}