using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IReservationService
    {
        Task<Result<bool>> IsReservationOwner(int reservationId);
        Task<Result<RestaurantPlanDTO>> GetRestaurantReservationPlan(GetRestaurantPlanDTO model);
        Task<Result<int>> CreateReservation(CreateRestaurantReservationDTO model);
        Task<Result> CancelReservation(int reservationId);
        Task<Result<ReservationInfoDTO>> GetReservationInfo(GetReservationInfoDTO model);
        Task<Result<IReadOnlyCollection<ReservationInfoDTO>>> GetReservations(GetReservationsDTO model);
    }
}