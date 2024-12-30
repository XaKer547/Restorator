using System.Collections.Immutable;
using FluentResults;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Enums;
using Restorator.Domain.Services;

namespace Restorator.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RestoratorDbContext _context;
        public ReservationService(RestoratorDbContext context)
        {
            _context = context;
        }

        public async Task<Result> CancelReservation(CancelReservationDTO cancelReservation)
        {
            var reseravation = _context.Reservations.Include(r => r.User)
                .SingleOrDefault(r => r.Id == cancelReservation.ReservationId);

            if (reseravation is null)
                return Result.Fail("Бронирование не найдено");

            var user = _context.Users.AsNoTracking()
                .Include(u => u.Role)
                .SingleOrDefault(u => u.Id == cancelReservation.UserId);

            if (user.Role != Roles.Manager)
                if (reseravation.User.Id != user.Id)
                    return Result.Fail("");

            reseravation.Canceled = true;

            _context.Reservations.Update(reseravation);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result<ReservationInfoDTO>> GetReservation(GetReservationInfoDTO getReservationInfo)
        {
            var reservation = await _context.Reservations.Include(r => r.Restaurant)
                .Include(r => r.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(reservation => reservation.Restaurant.Id == getReservationInfo.RestaurantId && !reservation.Canceled && reservation.User.Id == getReservationInfo.UserId
                && getReservationInfo.ReservationStartDate >= reservation.ReservationStart && getReservationInfo.ReservationStartDate <= reservation.ReservationEnd
                || getReservationInfo.ReservationEndDate >= reservation.ReservationStart && getReservationInfo.ReservationEndDate <= reservation.ReservationEnd);

            if (reservation is null)
                return Result.Fail("Бронирование не найдено");

            var info = new ReservationInfoDTO
            {
                Id = reservation.Id,
                UserId = reservation.User.Id,
                Username = reservation.User.Username,
                RestaurantId = reservation.Restaurant.Id,
                RestaurantName = reservation.Restaurant.Name,
                ReservationStart = reservation.ReservationStart,
                ReservationEnd = reservation.ReservationEnd
            };

            return Result.Ok(info);
        }
        public async Task<Result<IReadOnlyCollection<ReservationInfoDTO>>> GetReservations(GetReservationsDTO getReservations)
        {
            var predicate = PredicateBuilder.New<Reservation>(r => (r.ReservationEnd.Date == getReservations.SelectedDate.Date || r.ReservationStart.Date == getReservations.SelectedDate.Date));

            if (getReservations.UserId.HasValue)
                predicate = predicate.And(r => r.User.Id == getReservations.UserId.Value);

            if (getReservations.RestaurantId.HasValue)
                predicate = predicate.And(r => r.Restaurant.Id == getReservations.RestaurantId);

            if (getReservations.SkipCanceled.HasValue)
                predicate = predicate.And(r => r.Canceled != getReservations.SkipCanceled.Value);

            return await _context.Reservations
                 .AsNoTracking()
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

        public async Task<Result<RestaurantPlanDTO>> GetRestaurantPlan(GetRestaurantPlanDTO getRestaurantPlan)
        {
            var reservations = _context.Reservations.AsNoTracking()
                .Where(reservation => reservation.Restaurant.Id == getRestaurantPlan.RestaurantId && !reservation.Canceled
                && getRestaurantPlan.ReservationStartDate >= reservation.ReservationStart && getRestaurantPlan.ReservationStartDate <= reservation.ReservationEnd
                || getRestaurantPlan.ReservationEndDate >= reservation.ReservationStart && getRestaurantPlan.ReservationEndDate <= reservation.ReservationEnd)
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
                        State = CheckState(reservations, t.Id, getRestaurantPlan.UserId),
                    }).ToArray()
                }).SingleOrDefaultAsync(r => r.Id == getRestaurantPlan.RestaurantId);

            if (plan is null)
                return Result.Fail("Ресторан не найден");

            return Result.Ok(plan);
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

        public async Task<Result<bool>> ReservedTableBelongsToUser(int reservationId, int userId)
        {
            if (!_context.Users.Any(u => u.Id == userId))
                return Result.Fail("Пользователя не существует");

            if (!_context.Reservations.Any(r => r.Id == reservationId))
                return Result.Fail("Бронирование не существует");

            var belongsToUser = await _context.Reservations.AnyAsync(r => r.User.Id == userId && r.Id == reservationId);

            return Result.Ok(belongsToUser);
        }

        public async Task<Result> ReserveTables(CreateRestaurantReservationDTO reserveTable)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == reserveTable.UserId);

            if (user is null)
                return Result.Fail("Пользователя не существует");

            var tables = await _context.Tables.Where(t => reserveTable.ReservedTables.Contains(t.Id)).ToListAsync();

            if (tables is null || tables.Count != reserveTable.ReservedTables.Count)
                return Result.Fail("Стол не найден");

            var restaurant = await _context.Restaurants.
                SingleOrDefaultAsync(r => r.Id == reserveTable.RestaurantId && r.Template.Tables.All(t => tables.Contains(t)));

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            foreach (var table in tables)
            {
                var reservation = new Reservation()
                {
                    User = user,
                    Restaurant = restaurant,
                    Table = table,
                    ReservationStart = reserveTable.ReservationStartDate,
                    ReservationEnd = reserveTable.ReservationEndDate,
                };

                _context.Reservations.Add(reservation);
            }

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
