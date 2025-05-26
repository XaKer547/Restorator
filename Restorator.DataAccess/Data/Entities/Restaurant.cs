using System.ComponentModel.DataAnnotations.Schema;

namespace Restorator.DataAccess.Data.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<RestaurantImage> Images { get; set; } = new HashSet<RestaurantImage>();
        public string? MenuImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeOnly BeginWorkTime { get; set; }
        public TimeOnly EndWorkTime { get; set; }
        public bool Approved { get; set; } = false;

        [ForeignKey(nameof(Template))]
        public int TemplateId { get; set; }
        public RestaurantTemplate Template { get; set; }
        public ICollection<RestaurantTag> Tags { get; set; } = new HashSet<RestaurantTag>();
        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}