using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IReservationService
    {
        Task<Result<bool>> ReservedTableBelongsToUser(int reservationId, int userId);
        Task<Result<RestaurantPlanDTO>> GetRestaurantPlan(int restaurantId);
        Task<Result> ReserveTable(ReserveTableDTO reserveTable);
        Task<Result> CancelReservation(int reservationId);
    }
}