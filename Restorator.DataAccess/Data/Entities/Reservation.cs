namespace Restorator.DataAccess.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
        public Table Table { get; set; }
        public DateTime ReservationDateTime { get; set; }
    }
}