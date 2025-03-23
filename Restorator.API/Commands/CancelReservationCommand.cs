using FluentResults;
using MediatR;

namespace Restorator.API.Commands
{
    public class CancelReservationCommand : IRequest<Result>
    {
        public int ReservationId { get; }
        public CancelReservationCommand(int reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
