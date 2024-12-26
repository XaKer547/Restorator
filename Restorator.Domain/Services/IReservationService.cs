using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IReservationService
    {
        Task<Result<bool>> ReservedTableBelongsToUser(int reservationId, int userId);
        Task<Result<RestaurantPlanDTO>> GetRestaurantPlan(GetRestaurantPlanDTO getRestaurantPlan);
        Task<Result> ReserveTables(CreateRestaurantReservationDTO reserveTable);
        Task<Result> CancelReservation(CancelReservationDTO cancelReservation);
    }
}