namespace Restorator.Domain.Models
{
    public class GetRestaurantPlanDTO
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
    }
}
