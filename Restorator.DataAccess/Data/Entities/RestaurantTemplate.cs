namespace Restorator.DataAccess.Data.Entities
{
    /// <summary>
    /// Шаблон ресторана
    /// </summary>
    public class RestaurantTemplate
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool Deleted { get; set; } = false;
        public ICollection<Table> Tables { get; set; } = new HashSet<Table>();
        public ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}