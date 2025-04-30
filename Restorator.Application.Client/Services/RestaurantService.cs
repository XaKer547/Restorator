using System.Net.Http.Json;
using FluentResults;
using Restorator.Application.Client.Extensions;
using Restorator.Application.Client.Helpers;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient _client;
        public RestaurantService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Result> ChangeRestaurantApproval(ChangeRestaurantApprovalDTO model)
        {
            var response = await _client.PatchAsJsonAsync($"{model.RestaurantId}/approve", model.Approval);

            return await response.AsResult();
        }

        public async Task<Result<int>> CreateRestaurant(CreateRestaurantDTO model)
        {
            var response = await _client.PostAsJsonAsync(string.Empty, model);

            return await response.AsResult<int>();
        }

        public async Task<Result> DeleteRestaurant(int restaurantId)
        {
            var response = await _client.DeleteAsync($"{restaurantId}");

            return await response.AsResult();
        }

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetOwnedRestaurantPreviews()
        {
            var restaurants = await _client.GetFromJsonAsync<IReadOnlyCollection<RestaurantPreviewDTO>>("owned");

            return restaurants ?? [];
        }

        public async Task<Result<RestaurantInfoDTO>> GetRestaurantInfo(int restaurantId)
        {
            var info = await _client.GetFromJsonAsync<RestaurantInfoDTO>($"{restaurantId}/");

            return info.ToResultWithNullCheck();
        }

        public async Task<IReadOnlyCollection<RestaurantSearchItemDTO>> SearchRestaurants(string? name, CancellationToken cancellationToken = default)
        {
            var names = await _client.GetFromJsonAsync<IReadOnlyCollection<RestaurantSearchItemDTO>>($"search?name={name}", cancellationToken);

            return names ?? [];
        }

        public async Task<PaginatedList<RestaurantPreviewDTO>> GetRestaurantPreviews(GetRestaurantsPreviewDTO model)
        {
            var paginationQuery = model.PaginationFilter.ToQueryString();

            string filterQuery = string.Empty;

            if (model.Filter is not null)
                filterQuery = model.Filter.ToQueryString();

            var previews = await _client.GetFromJsonAsync<PaginatedList<RestaurantPreviewDTO>>($"?{paginationQuery}&{filterQuery}");

            return previews ?? PaginatedList<RestaurantPreviewDTO>.Empty();
        }

        public async Task<IReadOnlyCollection<RestaurantTagDTO>> GetRestaurantsTags()
        {
            var tags = await _client.GetFromJsonAsync<IReadOnlyCollection<RestaurantTagDTO>>("tags");

            return tags ?? [];
        }

        public async Task<IReadOnlyCollection<RestaurantTemplateDTO>> GetRestaurantTemplates()
        {
            var templates = await _client.GetFromJsonAsync<IReadOnlyCollection<RestaurantTemplateDTO>>("templates");

            return templates ?? [];
        }

        public async Task<Result> UpdateRestaurant(UpdateRestraurantDTO model)
        {
            var response = await _client.PutAsJsonAsync(string.Empty, model);

            return await response.AsResult();
        }

        public async Task<IReadOnlyCollection<RestaurantPreviewDTO>> GetLatestVisited()
        {
            var tags = await _client.GetFromJsonAsync<IReadOnlyCollection<RestaurantPreviewDTO>>("latest");

            return tags ?? [];
        }
    }
}