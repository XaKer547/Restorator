using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
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

        public async Task<Result> CancelReservation(int reservationId)
        {
            var reseravation = _context.Reservations.SingleOrDefault(r => r.Id == reservationId);

            if (reseravation is null)
                return Result.Fail("Бронирование не найдено");

            reseravation.Canceled = true;

            _context.Reservations.Update(reseravation);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result<RestaurantPlanDTO>> GetRestaurantPlan(GetRestaurantPlanDTO getRestaurantPlan)
        {
            var reservations = _context.Reservations
                .AsNoTracking()
                .Where(reservation => reservation.Restaurant.Id == getRestaurantPlan.RestaurantId
                && reservation.ReservationStart < getRestaurantPlan.ReservationEnd && reservation.ReservationEnd > getRestaurantPlan.ReservationStart)
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


            var tables = _context.Restaurants.AsNoTracking().Where(r => r.Id == plan.Id)
                 .Select(r => r.Template.Tables.Select(t => new TableDTO
                 {
                     Id = t.Id,
                     Height = t.Template.Height,
                     Width = t.Template.Width,
                     Rotation = t.Template.Rotation,
                     X = t.X,
                     Y = t.Y,
                 })).ToArray();

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
                    ReservationStart = reserveTable.ReservationDate,
                    ReservationEnd = reserveTable.ReservationDate.AddHours(reserveTable.Hours),
                };

                _context.Reservations.Add(reservation);
            }

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
