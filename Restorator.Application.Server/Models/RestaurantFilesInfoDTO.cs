namespace Restorator.Application.Server.Models
{
    public record RestaurantFilesInfoDTO
    {
        public string MenuPath { get; set; }
        public IEnumerable<string> ImagesPath { get; set; }
    }
}
