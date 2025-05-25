using FluentResults;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Restorator.Application.Server.Extensions;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;
using Roles = Restorator.DataAccess.Data.Entities.Enums.Roles;

namespace Restorator.Application.Server.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestoratorDbContext _context;
        private readonly IUserManager _userManager;
        public RestaurantService(RestoratorDbContext context,
                                 IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews()
        {
            if (!_userManager.TryGetUserId(out var userId))
                return [];

            return await _context.Restaurants.AsNoTracking()
                                             .Where(r => r.Owner.Id == userId)
                                             .Select(r => new RestaurantPreviewDTO
                                             {
                                                 Id = r.Id,
                                                 Name = r.Name,
                                                 Image = r.Images.Select(i => i.Image).FirstOrDefault(),
                                             }).ToListAsync();
        }
        public async Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Admin)
                return Result.Fail("У вас недостаточно прав, чтобы это сделать");

            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(r => r.Id == model.RestaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            restaurant.Approved = model.Approval;

            _context.Restaurants.Update(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO model)
        {
            var predicate = PredicateBuilder.New<Restaurant>(true);

            var filter = model.Filter;

            if (filter != null)
            {
                if (filter.RequireApproved.HasValue)
                    predicate = predicate.And(r => r.Approved == filter.RequireApproved);

                if (filter.TagId.HasValue)
                    predicate = predicate.And(r => r.Tags.Any(t => t.Id == filter.TagId));
            }
            var paginationFilter = model.PaginationFilter;

            return await _context.Restaurants.AsNoTracking()
                                             .OrderBy(r => r.Name)
                                             .Where(predicate)
                                             .Select(r => new RestaurantPreviewDTO
                                             {
                                                 Id = r.Id,
                                                 Name = r.Name,
                                                 Image = r.Images.Select(i => i.Image).FirstOrDefault(),
                                             }).AsPageAsync(paginationFilter.CurrentPage, paginationFilter.PageSize);
        }
        public async Task<Result<int>> CreateRestaurant(CreateRestaurantDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.AsNoTracking()
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return Result.Fail("Пользователя не существует");

            var tags = _context.RestaurantTags.AsNoTracking()
                                              .Where(t => model.Tags.Contains(t.Id));

            var restaurant = new Restaurant()
            {
                Owner = user,
                Name = model.Name,
                Description = model.Description,
                BeginWorkTime = model.BeginWorkTime,
                EndWorkTime = model.EndWorkTime,
                TemplateId = model.TemplateId,
                Images = [.. model.Images.Select(i => new RestaurantImage()
                {
                    Image = i
                })],
                MenuImage = model.Menu,
                Tags = [.. tags]
            };

            _context.Restaurants.Add(restaurant);

            await _context.SaveChangesAsync();

            return Result.Ok(restaurant.Id);
        }
        public async Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            var restaurant = await _context.Restaurants.AsNoTracking()
                                                       .Select(restaurant => new RestaurantInfoDTO()
                                                       {
                                                           Id = restaurant.Id,
                                                           Images = restaurant.Images.Select(i => i.Image),
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
                                                       })
                                                       .SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            return Result.Ok(restaurant);
        }
        public async Task<IReadOnlyCollection<RestaurantSearchItemDTO>> SearchRestaurants(string? name, CancellationToken cancellationToken = default)
        {
            return await _context.Restaurants.AsNoTracking()
                                             .Select(r => new RestaurantSearchItemDTO()
                                             {
                                                 Id = r.Id,
                                                 Name = r.Name,
                                             }).Where(x => x.Name.Contains(name ?? string.Empty))
                                             .ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantsTags()
        {
            return await _context.RestaurantTags.AsNoTracking()
                                                .Select(r => new RestaurantTagDTO()
                                                {
                                                    Id = r.Id,
                                                    Name = r.Name,
                                                }).ToListAsync();
        }
        public async Task<Result> DeleteRestaurant(int restaurantId)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var result = await GetRestaurantByPrivileges(restaurantId, userId, Roles.Admin, Roles.Manager);

            if (result.IsFailed)
                return result.ToResult();

            _context.Restaurants.Remove(result.Value);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<Result> UpdateRestaurant(UpdateRestraurantDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var result = await GetRestaurantByPrivileges(model.RestaurantId, userId, Roles.Admin, Roles.Manager);

            if (result.IsFailed)
                return result.ToResult();

            var restaurant = result.Value;

            restaurant.Name = model.Name;
            restaurant.Description = model.Description;

            restaurant.BeginWorkTime = model.BeginWorkTime;
            restaurant.EndWorkTime = model.EndWorkTime;

            restaurant.Images = [.. model.Images.Select(i => new RestaurantImage()
                {
                    Image = i
                })];

            restaurant.MenuImage = model.Menu;

            var tags = await _context.RestaurantTags.AsNoTracking()
                                                    .Where(t => model.Tags.Contains(t.Id))
                                                    .ToListAsync();

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
            return await _context.RestaurantTemplates.AsNoTracking()
                                                     .Select(t => new RestaurantTemplateDTO
                                                     {
                                                         Id = t.Id,
                                                         Scheme = t.Image,
                                                     }).ToListAsync();
        }
        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetLatestVisited() //latest = 30 days
        {
            if (!_userManager.TryGetUserId(out var userId))
                return [];

            return await _context.Reservations.AsNoTracking()
                .Where(r => r.User.Id == userId && DateTime.Today.Month < r.ReservationEnd.Month + 1)
                .Select(r => new RestaurantPreviewDTO
                {
                    Id = r.Restaurant.Id,
                    Name = r.Restaurant.Name,
                    Image = r.Restaurant.Images.Select(i => i.Image).FirstOrDefault(),
                }).ToListAsync();
        }
        public async Task<IReadOnlyCollection<RestaurantSearchItemDTO>> GetOwnedRestaurantsSearchItems()
        {
            if (!_userManager.TryGetUserId(out var userId))
                return [];

            return await _context.Restaurants.AsNoTracking()
                                             .Where(r => r.Owner.Id == userId)
                                             .Select(r => new RestaurantSearchItemDTO
                                             {
                                                 Id = r.Id,
                                                 Name = r.Name,
                                             }).ToListAsync();
        }

        private async Task<Result<Restaurant>> GetRestaurantByPrivileges(int restaurantId, int userId, params Roles[] roles)
        {
            var user = await _context.Users.AsNoTracking()
                                           .Include(x => x.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Не удалось получить id пользователя");

            if (!roles.Contains(user.Role))
                return Result.Fail("Недостаточно прав для выполнения операции");

            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            if (restaurant.Owner.Id != user.Id)
                return Result.Fail("Недостаточно прав для выполнения операции");

            return restaurant;
        }
    }
}