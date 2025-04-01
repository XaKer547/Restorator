using FluentResults;
using Restorator.Domain.Models.Account;
using Restorator.Domain.Models.Authorization;

namespace Restorator.Domain.Services
{
    public interface IAccountService
    {
        Task<Result<AuthorizationResult>> SignInAsync(SignInDTO model);
        Task<Result<AuthorizationResult>> SignInAsync(RecoverAccountDTO model);
        Task<Result<SessionInfo>> GetSessionInfoAsync();
        Task<Result> SignUpAsync(SignUpDTO model);
        Task<Result> RequestPasswordReset(string email);
        Task<Result> UpdatePassword(string password);
    }
}