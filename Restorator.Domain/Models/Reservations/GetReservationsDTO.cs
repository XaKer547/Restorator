namespace Restorator.Domain.Models.Reservations
{
    public class GetReservationsDTO
    {
        public DateTime SelectedDate { get; set; }
        public int? RestaurantId { get; set; }
        public int? UserId { get; set; }
        public bool? SkipCanceled { get; set; }
    }
}
