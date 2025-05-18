using FluentResults;
using Restorator.Application.Client.Extensions;
using Restorator.Application.Client.Services.Abstract;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;
using System.Text;

namespace Restorator.Application.Client.Services
{
    public class RestaurantService : ApiClientBase, IRestaurantService
    {
        public RestaurantService(HttpClient client) : base(client, "restaurant")
        { }

        public async Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO model)
        {
            var response = await PatchAsJsonAsync($"/{model.RestaurantId}/approve", model.Approval);

            return await response.AsResult();
        }

        public async Task<Result<int>> CreateRestaurant(CreateRestaurantDTO model)
        {
            var response = await PostAsJsonAsync(string.Empty, model);

            return await response.AsResult<int>();
        }

        public async Task<Result> DeleteRestaurant(int restaurantId)
        {
            var response = await DeleteAsync($"/{restaurantId}");

            return await response.AsResult();
        }

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews()
        {
            var restaurants = await GetFromJsonAsync<IReadOnlyCollection<RestaurantPreviewDTO>>("/owned");

            return restaurants ?? [];
        }

        public async Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            var info = await GetFromJsonAsync<RestaurantInfoDTO>($"/{restaurantId}");

            return info.ToResultWithNullCheck();
        }

        public async Task<IReadOnlyCollection<RestaurantSearchItemDTO>> SearchRestaurants(string? name, CancellationToken cancellationToken = default)
        {
            var names = await GetFromJsonAsync<IReadOnlyCollection<RestaurantSearchItemDTO>>($"/search?name={name}", cancellationToken);

            return names ?? [];
        }

        public async Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO model)
        {
            var pagintaion = model.PaginationFilter;

            var builder = new StringBuilder($"?PageSize={pagintaion.PageSize}&CurrentPage={pagintaion.CurrentPage}");

            var filter = model.Filter;

            if (filter is not null)
            {
                if (filter.TagId.HasValue)
                    builder.Append($"&tagId={filter.TagId}");

                if (filter.RequireApproved.HasValue)
                    builder.Append($"&requireApproved={filter.RequireApproved}");
            }

            var previews = await GetFromJsonAsync<PaginatedList<RestaurantPreviewDTO>>(builder.ToString());

            return previews ?? PaginatedList<RestaurantPreviewDTO>.Empty();
        }

        public async Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantsTags()
        {
            var tags = await GetFromJsonAsync<IReadOnlyCollection<RestaurantTagDTO>>("/tags");

            return tags ?? [];
        }

        public async Task<IReadOnlyCollection<RestaurantTemplateDTO>> GetRestaurantTemplates()
        {
            var templates = await GetFromJsonAsync<IReadOnlyCollection<RestaurantTemplateDTO>>("/templates");

            return templates ?? [];
        }

        public async Task<Result> UpdateRestaurant(UpdateRestraurantDTO model)
        {
            var response = await PutAsJsonAsync(string.Empty, model);

            return await response.AsResult();
        }

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetLatestVisited()
        {
            var tags = await GetFromJsonAsync<IReadOnlyCollection<RestaurantPreviewDTO>>("/latest");

            return tags ?? [];
        }
    }
}