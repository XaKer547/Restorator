using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IAccountService
    {
        Task<Result<AuthorizationResult>> SignInAsync(SignInDTO signIn);
        Task<Result<SessionInfo>> GetSessionInfoAsync(int userId);
        Task<Result> SignUpAsync(SignUpDTO signUp);
    }
}