using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IRestaurantService
    {
        Task<Result<int>> CreateRestaurant(CreateRestaurantDTO model);
        Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId);
        Task<IReadOnlyCollection<RestaurantSearchItemDTO>> GetRestaurantNames();
        Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantsTags();
        Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews();
        Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO model);
        Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO model);
        Task<Result> DeleteRestaurant(int restaurantId);
        Task<Result> UpdateRestaurant(UpdateRestraurantDTO model);
        Task<IReadOnlyCollection<RestaurantTemplateDTO>> GetRestaurantTemplates();
    }
}
