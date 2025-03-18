using FluentResults;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Enums;
using Restorator.Domain.Services;
using System.Collections.Immutable;

namespace Restorator.Application.Server.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RestoratorDbContext _context;
        private readonly IUserManager _userManager;
        public ReservationService(RestoratorDbContext context,
                                  IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result> CancelReservation(int reservationId)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var reseravation = await _context.Reservations.Include(r => r.User)
                                                          .SingleOrDefaultAsync(r => r.Id == reservationId);

            if (reseravation is null)
                return Result.Fail("Бронирование не найдено");

            var user = await _context.Users.AsNoTracking()
                                           .Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователь не найден");

            if (user.Role != Roles.Manager || reseravation.User.Id != user.Id)
                return Result.Fail("Недостаточно прав для отмены бронирования");

            reseravation.Canceled = true;

            _context.Reservations.Update(reseravation);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<Result<ReservationInfoDTO>> GetReservationInfo(GetReservationInfoDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя"); //CHECK

            var reservation = await _context.Reservations.AsNoTracking()
                                                .Select(r => new ReservationInfoDTO
                                                {
                                                    Id = r.Id,
                                                    UserId = r.User.Id,
                                                    Username = r.User.Username,
                                                    RestaurantId = r.Restaurant.Id,
                                                    RestaurantName = r.Restaurant.Name,
                                                    ReservationStart = r.ReservationStart,
                                                    ReservationEnd = r.ReservationEnd
                                                }).SingleOrDefaultAsync(r => r.Id == model.ReservationId);

            if (reservation is null)
                return Result.Fail("Бронирование не найдено");

            return Result.Ok(reservation);
        }
        public async Task<Result<IReadOnlyCollection<ReservationInfoDTO>>> GetReservations(GetReservationsDTO model)
        {
            var predicate = PredicateBuilder.New<Reservation>(r => r.ReservationEnd.Date == model.SelectedDate.Date
                                                                   || r.ReservationStart.Date == model.SelectedDate.Date);

            if (model.UserId.HasValue)
                predicate = predicate.And(r => r.User.Id == model.UserId.Value);

            if (model.RestaurantId.HasValue)
                predicate = predicate.And(r => r.Restaurant.Id == model.RestaurantId);

            if (model.SkipCanceled.HasValue)
                predicate = predicate.And(r => r.Canceled != model.SkipCanceled.Value);

            return await _context.Reservations.AsNoTracking()
                                              .Where(predicate)
                                              .Select(r => new ReservationInfoDTO
                                              {
                                                  Id = r.Id,
                                                  UserId = r.User.Id,
                                                  Username = r.User.Username,
                                                  RestaurantId = r.Restaurant.Id,
                                                  RestaurantName = r.Restaurant.Name,
                                                  ReservationStart = r.ReservationStart,
                                                  ReservationEnd = r.ReservationEnd,
                                                  Canceled = r.Canceled
                                              }).ToListAsync();
        }
        public async Task<Result<RestaurantPlanDTO>> GetRestaurantReservationPlan(GetRestaurantPlanDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var predicate = PredicateBuilder.New<Reservation>(r => r.Restaurant.Id == model.RestaurantId
                                                                   && !r.Canceled
                                                                   && ((model.ReservationStartDate >= r.ReservationStart
                                                                        && model.ReservationStartDate <= r.ReservationEnd) || (model.ReservationEndDate >= r.ReservationStart
                                                                                                                               && model.ReservationEndDate <= r.ReservationEnd)));

            var reservations = _context.Reservations.AsNoTracking()
                                                    .Where(predicate)
                                                    .Select(r => new ReserveTableDTO
                                                    {
                                                        UserId = r.User.Id,
                                                        TableId = r.Table.Id,
                                                    });


            var plan = await _context.Restaurants.AsNoTracking()
                                                 .Select(restaurant => new RestaurantPlanDTO
                                                 {
                                                     Id = restaurant.Id,
                                                     Scheme = restaurant.Template.Image,
                                                     BeginWorkTime = restaurant.BeginWorkTime,
                                                     EndWorkTime = restaurant.EndWorkTime,
                                                     Tables = restaurant.Template.Tables.Select(t => new TableDTO
                                                     {
                                                         Id = t.Id,
                                                         Height = t.Template.Height,
                                                         Width = t.Template.Width,
                                                         Rotation = t.Template.Rotation,
                                                         X = t.X,
                                                         Y = t.Y,
                                                         State = CheckState(reservations, t.Id, userId),
                                                     }).ToArray()
                                                 }).SingleOrDefaultAsync(r => r.Id == model.RestaurantId);

            if (plan is null)
                return Result.Fail("Ресторан не найден");

            return Result.Ok(plan);
        }
        public async Task<Result<bool>> IsReservationOwner(int reservationId)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            if (!await _context.Users.AnyAsync(u => u.Id == userId))
                return Result.Fail("Пользователя не существует");

            if (!await _context.Reservations.AnyAsync(r => r.Id == reservationId))
                return Result.Fail("Бронирование не существует");

            return Result.Ok(await _context.Reservations.AnyAsync(r => r.User.Id == userId && r.Id == reservationId));
        }
        public async Task<Result<int>> CreateReservation(CreateRestaurantReservationDTO model)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователя не существует");

            var tables = await _context.Tables.Where(t => model.ReservedTables.Contains(t.Id))
                                              .ToListAsync();

            if (tables is null || tables.Count != model.ReservedTables.Count)
                return Result.Fail("Стол не найден");

            var restaurant = await _context.Restaurants.SingleOrDefaultAsync(r => r.Id == model.RestaurantId);

            //задачка Aka "Этот стол принадлежит этому ресторану?"
            //&& r.Template.Tables.All(t => tables.Contains(t)));

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            foreach (var table in tables)
            {
                var reservation = new Reservation()
                {
                    User = user,
                    Restaurant = restaurant,
                    Table = table,
                    ReservationStart = model.ReservationStartDate,
                    ReservationEnd = model.ReservationEndDate,
                };

                _context.Reservations.Add(reservation);
            }

            await _context.SaveChangesAsync();

            return Result.Ok(restaurant.Id);
        }
        private static TableStates CheckState(IEnumerable<ReserveTableDTO> reserveTable, int tableId, int userId)
        {
            var reservation = reserveTable.FirstOrDefault(r => r.TableId == tableId);

            if (reservation is null)
                return TableStates.Avaible;
            else if (reservation.UserId == userId)
                return TableStates.OccupiedByUser;

            return TableStates.OccupiedByOther;
        }
    }
}