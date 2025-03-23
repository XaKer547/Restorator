using FluentResults;
using Refit;
using Restorator.Domain.Models.Authorization;

namespace Restorator.Domain.Services
{
    public interface IAccountService
    {
        Task<Result<AuthorizationResult>> SignInAsync(SignInDTO model);
        Task<Result<SessionInfo>> GetSessionInfoAsync();
        Task<Result> SignUpAsync(SignUpDTO model);
    }
}