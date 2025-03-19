using FluentResults;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class ReservationService : IReservationService
    {
        public Task<Result> CancelReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> CreateReservation(CreateRestaurantReservationDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ReservationInfoDTO>> GetReservationInfo(GetReservationInfoDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IReadOnlyCollection<ReservationInfoDTO>>> GetReservations(GetReservationsDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RestaurantPlanDTO>> GetRestaurantReservationPlan(GetRestaurantPlanDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> IsReservationOwner(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
