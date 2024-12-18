using Restorator.Domain.Models;

namespace Restorator.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<AuthorizationResult> SignInAsync(SignInDTO signIn);
        Task<bool> SignUpAsync(SignUpDTO signUp);

    }
}