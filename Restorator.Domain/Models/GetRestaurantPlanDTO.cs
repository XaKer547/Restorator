namespace Restorator.Domain.Models
{
    public class GetRestaurantPlanDTO
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
    }
}
