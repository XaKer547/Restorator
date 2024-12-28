using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IRestaurantService
    {
        Task<Result> CreateRestaurant(CreateRestaurantDTO createRestaurant);
        Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId);
        Task<IReadOnlyCollection<RestaurantSearchItemDTO>> GetRestaurantNames();
        Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantTags();
        Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews(GetOwnedRestaurantsPreviewDTO getRestaurantsPreview);
        Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO getRestaurantsPreview);
        Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO changeRestaurantApproval);
        Task<Result> DeleteRestaurant(int restaurantId);
        Task<Result> UpdateRestaurant(UpdateRestraurantDTO updateRestraurantDTO);
    }
}
