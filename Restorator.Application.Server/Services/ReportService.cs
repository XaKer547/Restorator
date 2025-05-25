using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.Domain.Models.Reports;
using Restorator.Domain.Services;
using System.Globalization;

namespace Restorator.Application.Server.Services
{
    public record RestaurantDayReservation
    {
        public string RestaurantName { get; init; }
        public int DayOfWeek { get; init; }
        public int Count { get; init; }
    }
    public record ReservationsStatus
    {
        public int Canceled { get; init; }
        public int Waiting { get; init; }
        public int Finished { get; init; }
    }

    public class ReportService : IReportService
    {
        private readonly RestoratorDbContext _context;
        private readonly IUserManager _userManager;
        public ReportService(RestoratorDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<MonthSummaryReportDTO> GetMonthSummaryReport(DateOnly date, int? restaurantId = default)
        {
            if (!_userManager.TryGetUserId(out var userId))
                throw new ArgumentNullException($"Не знаю как, но userId is null");

            string restaurantIdParameter;

            if (restaurantId.HasValue)
                restaurantIdParameter = restaurantId.Value.ToString();
            else
                restaurantIdParameter = "NULL";

            var queryParametersBody = $"{userId}, '{date:yyyy-MM-dd}', {restaurantIdParameter}";

            var restaurantDayReservations = _context.Database.SqlQueryRaw<RestaurantDayReservation>
                (
                $"SELECT * FROM GetReservationsGroupedFiltered({queryParametersBody})"
                );

            var restaurantDailyReservations = await GetRestaurantDailyReservationReports(restaurantDayReservations);

            if (!restaurantDailyReservations.Any())
                return new MonthSummaryReportDTO
                {
                    IsEmpty = true
                };

            var mostPopularDay = await restaurantDayReservations.OrderByDescending(x => x.Count)
                                                                .Select(x => x.DayOfWeek)
                                                                .FirstAsync();

            var mostPopularRestaurant = restaurantDailyReservations.Select(x => new MonthPopularRestaurantReportDTO
            {
                RestaurantName = x.RestaurantName,
                Count = x.Reservations.Aggregate((x, y) => x + y),
            }).OrderByDescending(x => x.Count)
            .First();

            var reservationsRateReport = await _context.Database.SqlQueryRaw<ReservationsRateReportDTO>(
                $"SELECT * FROM GetMostReservedDay({queryParametersBody})"
                ).OrderByDescending(x => x.Rate)
                .FirstAsync();

            var reservationStatuses = await _context.Database.SqlQueryRaw<ReservationsStatus>(
                $"SELECT * FROM GetMonthReservationsStatuses({queryParametersBody})"
                ).FirstAsync();

            return new MonthSummaryReportDTO
            {
                ReservationsRate = reservationsRateReport,
                MostPopularRestaurant = mostPopularRestaurant,
                RestaurantDailyReservations = restaurantDailyReservations,
                Canceled = reservationStatuses.Canceled,
                Finished = reservationStatuses.Finished,
                Reserved = reservationStatuses.Waiting,
                IsEmpty = false,
                MostPopularDay = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName((DayOfWeek)mostPopularDay - 1)
            };
        }

        private async Task<IEnumerable<RestaurantDailyReservationReportDTO>> GetRestaurantDailyReservationReports(IQueryable<RestaurantDayReservation> restaurantDayReservations)
        {
            var allDays = Enumerable.Range(1, 7);

            var template = from day in allDays
                           from resGroup in restaurantDayReservations
                           select new RestaurantDayReservation
                           {
                               DayOfWeek = day,
                               Count = 0,
                               RestaurantName = resGroup.RestaurantName
                           };

            var resultDict = await restaurantDayReservations.ToDictionaryAsync(r => (r.DayOfWeek, r.RestaurantName), r => r.Count);

            return template.GroupBy(x => new { x.DayOfWeek, x.RestaurantName })
                .Select(g =>
                {
                    int count = 0;

                    if (g.Key != null && resultDict.TryGetValue((g.Key.DayOfWeek, g.Key.RestaurantName), out int existingCount))
                    {
                        count = existingCount;
                    }

                    return new
                    {
                        g.Key.DayOfWeek,
                        Count = count,
                        g.Key.RestaurantName
                    };
                })
                .OrderBy(x => x.DayOfWeek)
                .GroupBy(x => x.RestaurantName)
                .Select(x => new RestaurantDailyReservationReportDTO
                {
                    RestaurantName = x.Key,
                    Reservations = x.SelectMany(x => new int[] { x.Count })
                });
        }
    }
}
