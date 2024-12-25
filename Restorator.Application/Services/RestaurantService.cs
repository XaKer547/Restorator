using FluentResults;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Restorator.Application.Services.Extensions;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;
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

        public async Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO changeRestaurantApproval)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == changeRestaurantApproval.UserId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Admin)
                return Result.Fail("У вас недостаточно прав, чтобы это сделать");

            var restaurant = _context.Restaurants.SingleOrDefault(r => r.Id == changeRestaurantApproval.RestaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            restaurant.Approved = changeRestaurantApproval.Approval;

            _context.Restaurants.Update(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO getRestaurantsPreview)
        {
            var filter = getRestaurantsPreview.Filter;

            var predicate = PredicateBuilder.New<Restaurant>(false);

            var original = predicate;

            if (filter.RequireApproved.HasValue)
                predicate = predicate.And(r => r.Approved == filter.RequireApproved);

            if (filter.Tag is not null)
                predicate = predicate.And(r => r.Tags.Any(t => t.Id == filter.Tag.Id));

            return await _context.Restaurants.AsNoTracking()
                .Where(predicate)
                .Select(r => new RestaurantPreviewDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Image = r.Image,
                }).AsPageAsync(getRestaurantsPreview.CurrentPage, getRestaurantsPreview.PageSize);
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
                Image = createRestaurant.Image,
                MenuImage = createRestaurant.Menu,
            };

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            var restaurant = await _context.Restaurants.AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            var info = new RestaurantInfoDTO()
            {
                Id = restaurant.Id,
                Image = restaurant.Image,
                Menu = restaurant.MenuImage,
                Name = restaurant.Name,
                Description = restaurant.Description,
                BeginWorkTime = restaurant.BeginWorkTime,
                EndWorkTime = restaurant.EndWorkTime,
            };

            return Result.Ok(info);
        }
        public async Task<IReadOnlyCollection<RestaurantSearchItemDTO>> GetRestaurantNames()
        {
            return await _context.Restaurants.AsNoTracking()
                .Select(r => new RestaurantSearchItemDTO()
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantTags()
        {
            return await _context.RestaurantTags.AsNoTracking()
               .Select(r => new RestaurantTagDTO()
               {
                   Id = r.Id,
                   Name = r.Name,
               }).ToArrayAsync();
        }
    }
}