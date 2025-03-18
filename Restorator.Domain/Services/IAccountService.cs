using FluentResults;
using Refit;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IAccountService
    {
        [Post("")]
        Task<Result<AuthorizationResult>> SignInAsync(SignInDTO signIn);


        [Get("info")]
        [Headers("Authorization: Bearer")]
        Task<Result<SessionInfo>> GetSessionInfoAsync();


        [Post("new")]
        Task<Result> SignUpAsync(SignUpDTO signUp);
    }
}