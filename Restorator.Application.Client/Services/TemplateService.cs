using System.Net.Http.Json;
using FluentResults;
using Restorator.Application.Client.Extensions;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly HttpClient _client;
        public TemplateService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Result<int>> CreateRestaurantTemplate(CreateRestaurantTemplateDTO model)
        {
            var response = await _client.PostAsJsonAsync("table", model);

            return await response.AsResult<int>();
        }

        public async Task<Result<int>> CreateTableTemplate(CreateTableTempateDTO model)
        {
            var response = await _client.PostAsJsonAsync("restaurant", model);

            return await response.AsResult<int>();
        }

        public Task<Result> DeleteRestaurantTemplate(int restaurantTemplateId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<RestaurantTemplatePreview>> GetRestaurantsTemplatePreview()
        {
            var templatePreviews = await _client.GetFromJsonAsync<IReadOnlyCollection<RestaurantTemplatePreview>>("restaurant");

            return templatePreviews ?? [];
        }

        public async Task<RestaurantTemplateDTO> GetRestaurantTemplate(int restaurantTemplateId)
        {
            var template = await _client.GetFromJsonAsync<RestaurantTemplateDTO>($"restaurant/{restaurantTemplateId}");

            return template;
        }

        public async Task<IReadOnlyCollection<TableTemplateDTO>> GetTableTemplates()
        {
            var templates = await _client.GetFromJsonAsync<IReadOnlyCollection<TableTemplateDTO>>("tables");

            return templates ?? [];
        }

        Task<Result<RestaurantTemplateDTO>> ITemplateService.GetRestaurantTemplate(int restaurantTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}