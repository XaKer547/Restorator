using FluentResults;
using Restorator.Application.Client.Extensions;
using Restorator.Domain.Models.Authorization;
using Restorator.Domain.Services;
using System.Net.Http.Json;

namespace Restorator.Application.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly ISessionManager _sessionManager;

        public AccountService(HttpClient client, ISessionManager sessionManager)
        {
            _client = client;
            _sessionManager = sessionManager;
        }

        public async Task<Result<SessionInfo>> GetSessionInfoAsync()//Why?
        {
            var sessionInfo = await _client.GetFromJsonAsync<SessionInfo>("info");

            return sessionInfo.ToResultWithNullCheck();
        }
        public async Task<Result<AuthorizationResult>> SignInAsync(SignInDTO model)
        {
            var response = await _client.PostAsJsonAsync("signin", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return Result.Fail(error);
            }

            var result = await response.Content.ReadFromJsonAsync<AuthorizationResult>();

            if (result is null)
                return Result.Fail("");

            _sessionManager.SetSession(result.SessionInfo, result.Token);

            return result;
        }
        public async Task<Result> SignUpAsync(SignUpDTO model)
        {
            var response = await _client.PostAsJsonAsync("signup", model);

            return await response.AsResult();
        }
    }
}