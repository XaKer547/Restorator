namespace Restorator.Domain.Models
{
    public class CreateRestaurantReservationDTO
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public IReadOnlyCollection<int> ReservedTables { get; set; }
        public DateTime ReservationDate { get; set; }
        public int Hours { get; set; }
    }
}