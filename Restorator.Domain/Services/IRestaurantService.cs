using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IRestaurantService
    {
        Task CreateRestaurant(CreateRestaurantDTO createRestaurant);
        Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO getRestaurantsPreview);
    }
}
