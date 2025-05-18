namespace Restorator.Domain.Models.Restaurant
{
    public class RestaurantInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeOnly BeginWorkTime { get; set; }
        public TimeOnly EndWorkTime { get; set; }
        public IEnumerable<byte[]> Images { get; set; }
        public byte[]? Menu { get; set; }
        public bool Approved { get; set; }
        public IEnumerable<RestaurantTagDTO> Tags { get; set; }
    }
}