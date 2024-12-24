using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.Application.Services.Extensions;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
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

        public async Task<Result> CreateRestaurant(CreateRestaurantDTO createRestaurant)
        {
            var restaurant = new Restaurant()
            {
                Name = createRestaurant.Name,
                Description = createRestaurant.Description,
                BeginWorkTime = createRestaurant.BeginWorkTime,
                EndWorkTime = createRestaurant.EndWorkTime,
                TemplateId = createRestaurant.TemplateId,
            };

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            var info = new RestaurantInfoDTO()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                BeginWorkTime = restaurant.BeginWorkTime,
                EndWorkTime = restaurant.EndWorkTime,
            };

            return Result.Ok(info);
        }
    }
}