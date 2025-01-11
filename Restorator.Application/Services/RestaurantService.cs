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

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews(GetOwnedRestaurantsPreviewDTO getRestaurantsPreview)
        {
            return await _context.Restaurants.AsNoTracking()
                .Where(r => r.Owner.Id == getRestaurantsPreview.UserId)
                .Select(r => new RestaurantPreviewDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Image = r.Image,
                }).ToArrayAsync();
        }
        public async Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO changeRestaurantApproval)
        {
            var user = _context.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == changeRestaurantApproval.UserId);

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
            var predicate = PredicateBuilder.New<Restaurant>(true);

            var searchFilter = getRestaurantsPreview.Filter;

            if (searchFilter is not null)
            {
                if (searchFilter.RequireApproved.HasValue)
                    predicate = predicate.And(r => r.Approved == searchFilter.RequireApproved);

                if (searchFilter.Tag is not null)
                    predicate = predicate.And(r => r.Tags.Any(t => t.Id == searchFilter.Tag.Id));
            }

            var paginationFilter = getRestaurantsPreview.PaginationFilter;

            return await _context.Restaurants.AsNoTracking()
                .Where(predicate)
                .Select(r => new RestaurantPreviewDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Image = r.Image,
                }).AsPageAsync(paginationFilter.CurrentPage, paginationFilter.PageSize);
        }
        public async Task<Result> CreateRestaurant(CreateRestaurantDTO createRestraurant)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == createRestraurant.UserId);

            if (user == null)
                return Result.Fail("Пользователя не существует");

            var tags = _context.RestaurantTags.Where(t => createRestraurant.Tags.Contains(t.Id));

            var restaurant = new Restaurant()
            {
                Owner = user,
                Name = createRestraurant.Name,
                Description = createRestraurant.Description,
                BeginWorkTime = createRestraurant.BeginWorkTime,
                EndWorkTime = createRestraurant.EndWorkTime,
                TemplateId = createRestraurant.TemplateId,
                Image = createRestraurant.Image,
                MenuImage = createRestraurant.Menu,
                Tags = [.. tags]
            };

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            var restaurant = await _context.Restaurants.Include(r => r.Tags)
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            var info = new RestaurantInfoDTO()
            {
                Id = restaurant.Id,
                Image = restaurant.Image,
                Menu = restaurant.MenuImage,
                Name = restaurant.Name,
                Approved = restaurant.Approved,
                Description = restaurant.Description,
                BeginWorkTime = restaurant.BeginWorkTime,
                EndWorkTime = restaurant.EndWorkTime,
                Tags = restaurant.Tags.Select(t => new RestaurantTagDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
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
        public async Task<Result> DeleteRestaurant(int restaurantId)
        {
            var restaurant = _context.Restaurants.SingleOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> UpdateRestaurant(UpdateRestraurantDTO updateRestraurant)
        {
            var restaurant = _context.Restaurants.Include(r => r.Tags)
                .SingleOrDefault(r => r.Id == updateRestraurant.RestaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            restaurant.Name = updateRestraurant.Name;
            restaurant.Description = updateRestraurant.Description;

            restaurant.BeginWorkTime = updateRestraurant.BeginWorkTime;
            restaurant.EndWorkTime = updateRestraurant.EndWorkTime;

            restaurant.Image = updateRestraurant.Image;

            restaurant.MenuImage = updateRestraurant.Menu;

            var tags = _context.RestaurantTags.Where(t => updateRestraurant.Tags.Contains(t.Id))
                .ToList();

            restaurant.Tags.Clear();

            foreach (var tag in tags)
            {
                if (restaurant.Tags.Any(t => t.Id == tag.Id))
                    continue;

                restaurant.Tags.Add(tag);
            }

            _context.Restaurants.Update(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<IReadOnlyCollection<RestaurantTemplateDTO>> GetRestaurantTemplates()
        {
            return await _context.RestaurantTemplates.Select(t => new RestaurantTemplateDTO
            {
                Id = t.Id,
                Image = t.Image,
            }).ToListAsync();
        }
    }
}