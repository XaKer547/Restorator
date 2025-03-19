using FluentResults;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IAccountService _accountService;
        public RestaurantService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> CreateRestaurant(CreateRestaurantDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews()
        {
            throw new NotImplementedException();
        }

        public Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<RestaurantSearchItemDTO>> GetRestaurantNames()
        {
            var a = await _accountService.SignInAsync(new SignInDTO()
            {
                Login = "Manager",
                Password = "Manager"
            });

            return [];
        }

        public Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantsTags()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RestaurantTemplateDTO>> GetRestaurantTemplates()
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateRestaurant(UpdateRestraurantDTO model)
        {
            throw new NotImplementedException();
        }
    }
}