using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.Domain.Models;
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

        public async Task<Result<RestaurantPlanDTO>> GetRestaurantPlan(int restaurantId)
        {
            var plan = await _context.Restaurants.Select(r => new RestaurantPlanDTO
            {
                Id = r.Id,
                Scheme = r.Template.Image,
                BeginWorkTime = r.BeginWorkTime,
                EndWorkTime = r.EndWorkTime,
                Tables = r.Template.Tables.Select(t => new TableDTO
                {
                    Id = t.Id,
                    Height = t.Template.Height,
                    Width = t.Template.Width,
                    Rotation = t.Template.Rotation,
                    X = t.X,
                    Y = t.Y,
                }).ToArray()
            }).SingleOrDefaultAsync(r => r.Id == restaurantId);

            if (plan is null)
                return Result.Fail("Ресторан не найден");

            return Result.Ok(plan);
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

        public async Task<Result> ReserveTable(ReserveTableDTO reserveTable)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == reserveTable.UserId);

            if (user is null)
                return Result.Fail("Пользователя не существует");

            var table = await _context.Tables.SingleOrDefaultAsync(t => t.Id == reserveTable.TableId);

            if (table is null)
                return Result.Fail("Стол не найден");

            var restaurant = await _context.Restaurants.
                SingleOrDefaultAsync(r => r.Id == reserveTable.RestaurantId && r.Template.Tables.Any(t => t == table));

            if (restaurant is null)
                return Result.Fail("Ресторан не найден");

            var reservation = new Reservation()
            {
                User = user,
                Restaurant = restaurant,
                Table = table,
                ReservationStart = reserveTable.ReservationStart,
                ReservationEnd = reserveTable.ReservationEnd,
            };

            _context.Reservations.Add(reservation);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
