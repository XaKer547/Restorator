using System.Net.Http.Json;
using FluentResults;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        public AccountService(HttpClient client)
        {
            _client = client;
        }

        public Task<Result<SessionInfo>> GetSessionInfoAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AuthorizationResult>> SignInAsync(SignInDTO signIn)
        {
            var response = await _client.PostAsJsonAsync(string.Empty, signIn);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return Result.Fail(error);
            }

            //accept header
            //return some top data or get it from get on load?
            var result = await response.Content.ReadFromJsonAsync<AuthorizationResult>();

            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result!.Token);

            return Result.Ok();
        }

        public Task<Result> SignUpAsync(SignUpDTO signUp)
        {
            throw new NotImplementedException();
        }
    }
}