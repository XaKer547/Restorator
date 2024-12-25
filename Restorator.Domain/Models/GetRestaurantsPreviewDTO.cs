namespace Restorator.Domain.Models
{
    public class GetRestaurantsPreviewDTO
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public GetRestaurantsPreviewFilter Filter { get; set; }
    }
}
