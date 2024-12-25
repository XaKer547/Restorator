namespace Restorator.Domain.Models
{
    public class RestaurantPlanDTO
    {
        public int Id { get; set; }
        public byte[] Scheme { get; set; }
        public TimeOnly BeginWorkTime { get; set; }
        public TimeOnly EndWorkTime { get; set; }
        public IReadOnlyCollection<TableDTO> Tables { get; set; }
    }
}
