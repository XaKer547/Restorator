namespace Restorator.Domain.Models
{
    public class GetReservationInfoDTO
    {
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
    }
}
