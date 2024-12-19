using Restorator.DataAccess.Data;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestoratorDbContext _context;
        public RestaurantService(RestoratorDbContext context)
        {
            _context = context;
        }

        public Task CreateRestaurant(CreateRestaurantDTO createRestaurant)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetRestaurantPreviews()
        {

            return null;
        }
    }
}
