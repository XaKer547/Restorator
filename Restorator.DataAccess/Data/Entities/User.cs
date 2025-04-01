namespace Restorator.DataAccess.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Verified { get; set; } = false;
        public string? OTP { get; set; }
        public virtual Role Role { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}