using Restorator.DataAccess.Data;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class ReservationService : IReservationService
    {
        public ReservationService(RestoratorDbContext context)
        {
            _context = context;
        }
    }
}
