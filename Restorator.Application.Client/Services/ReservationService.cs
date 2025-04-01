using FluentResults;
using Restorator.Application.Client.Extensions;
using Restorator.Application.Client.Helpers;
using Restorator.Domain.Models.Reservations;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Services;
using System.Net.Http.Json;

namespace Restorator.Application.Client.Services
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _client;
        public ReservationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Result> CancelReservation(int reservationId)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"{reservationId}/cancel");

            var response = await _client.SendAsync(request);

            return await response.AsResult();
        }

        public async Task<Result<int>> CreateReservation(CreateRestaurantReservationDTO model)
        {
            var response = await _client.PostAsJsonAsync(string.Empty, model);

            return await response.AsResult<int>();
        }

        public async Task<Result<ReservationInfoDTO>> GetReservationInfo(int reservationId)
        {
            var plan = await _client.GetFromJsonAsync<ReservationInfoDTO>($"{reservationId}");

            return plan.ToResultWithNullCheck();
        }

        public async Task<Result<IReadOnlyCollection<ReservationInfoDTO>>> GetReservations(GetReservationsDTO model)
        {
            var queryString = model.ToQueryString();

            var reservations = await _client.GetFromJsonAsync<IReadOnlyCollection<ReservationInfoDTO>>($"filter?{queryString}");

            return reservations.ToResultWithNullCheck();
        }

        public async Task<Result<RestaurantPlanDTO>> GetRestaurantReservationPlan(GetRestaurantPlanDTO model)
        {
            var plan = await _client.GetFromJsonAsync<RestaurantPlanDTO>($"{model.RestaurantId}/plan?ReservationStartDate={model.ReservationStartDate}&ReservationEndDate={model.ReservationEndDate}");

            return plan.ToResultWithNullCheck();
        }

        public Task<Result<bool>> IsReservationOwner(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
