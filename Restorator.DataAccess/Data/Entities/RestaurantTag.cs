namespace Restorator.DataAccess.Data.Entities
{
    public class RestaurantTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}
