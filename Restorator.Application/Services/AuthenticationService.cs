using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //private readonly RestoratorDbContext _context;
        public AuthenticationService(/*RestoratorDbContext context*/)
        {
            //_context = context;
        }

        public async Task<AuthorizationResult> SignInAsync(SignInDTO signIn)
        {
            //    var user = await _context.Users.Include(u => u.Role)
            //        .Where(u => u.Account.Password == signIn.Password && u.Account.Login == signIn.Login)
            //        .SingleOrDefaultAsync();

            //    if (user is null)
            return new AuthorizationResult()
            {
                Error = "Пользователь с такими данными не найден"
            };

            //var sessionInfo = new SessionInfo(user.Id, user.Username, user.Role.Name);

            //return new AuthorizationResult()
            //{
            //    SessionInfo = sessionInfo,
            //};
        }

        public Task<bool> SignUpAsync(SignUpDTO signUp)
        {
            return Task.FromResult(true);
        }
    }
}
