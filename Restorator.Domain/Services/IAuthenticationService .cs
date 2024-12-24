using FluentResults;
using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<Result<SessionInfo>> SignInAsync(SignInDTO signIn);
        Task<Result> SignUpAsync(SignUpDTO signUp);
    }
}