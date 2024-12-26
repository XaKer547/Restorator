namespace Restorator.Domain.Models
{
    public class CancelReservationDTO
    {
        public int UserId { get; set; }
        public int TableId { get; set; }
        public int RestaurantId { get; set; }

        //limits of reservation Date
    }
}
