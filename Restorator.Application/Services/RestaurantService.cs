using Microsoft.EntityFrameworkCore;
using Restorator.Application.Services.Extensions;
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

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO getRestaurantsPreview)
        {
            return await _context.Restaurants.Select(r => new RestaurantPreviewDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
            }).AsPage(getRestaurantsPreview.CurrentPage, getRestaurantsPreview.PageSize)
            .ToArrayAsync();
        }
    }
}