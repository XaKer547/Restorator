namespace Restorator.Domain.Models
{
    public class CreateRestaurantDTO
    {
        public string Name { get; set; }
        public int TemplateId { get; set; }
        public string Description { get; set; }
        public TimeOnly BeginWorkTime { get; set; }
        public TimeOnly EndWorkTime { get; set; }

        public byte[]? Image { get; set; }
        public byte[]? Menu { get; set; }
    }
}
