using FluentResults;
using MediatR;
using Restorator.API.Commands;
using Restorator.API.Infrastructure;
using Restorator.API.Models.MailTemplates;
using Restorator.Domain.Services;

namespace Restorator.API.CommandHandlers
{
    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, Result>
    {
        private readonly IReservationService _reservationService;
        private readonly MailService _mailService;
        public CancelReservationCommandHandler(IReservationService reservationService,
                                               MailService mailService)
        {
            _reservationService = reservationService;
            _mailService = mailService;
        }

        public async Task<Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var result = await _reservationService.CancelReservation(request.ReservationId);

            if (result.IsFailed)
                return result;

            var info = await _reservationService.GetReservationInfo(request.ReservationId);




            var template = new ReservationCanceledMailTemplate()
            {

            }




        }
    }
}
