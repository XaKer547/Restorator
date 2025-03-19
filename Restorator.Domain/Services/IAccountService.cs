using FluentResults;
using Refit;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IAccountService
    {
        [Post("/signin")]
        Task<Result<AuthorizationResult>> SignInAsync(SignInDTO signIn);


        [Get("/info")]
        [Headers("Authorization: Bearer")]
        Task<Result<SessionInfo>> GetSessionInfoAsync();


        [Post("/signup")]
        Task<Result> SignUpAsync(SignUpDTO signUp);
    }
}