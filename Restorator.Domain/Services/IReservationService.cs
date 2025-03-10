using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IReservationService
    {
        Task<Result<bool>> IsReservationOwner(int reservationId, int userId);
        Task<Result<RestaurantPlanDTO>> GetRestaurantPlan(int userId, GetRestaurantPlanDTO getRestaurantPlan);
        Task<Result> CreateReservation(int userId, CreateRestaurantReservationDTO reserveTable);
        Task<Result> CancelReservation(int userId, CancelReservationDTO cancelReservation);
        Task<Result<ReservationInfoDTO>> GetReservation(int userId, GetReservationInfoDTO getReservationInfo);
        Task<Result<IReadOnlyCollection<ReservationInfoDTO>>> GetReservations(GetReservationsDTO getReservations);
    }
}