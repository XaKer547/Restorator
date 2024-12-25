using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Data.Entities.Enums;
using Restorator.DataAccess.Extensions;
using Restorator.DataAccess.Helpers;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly RestoratorDbContext _context;
        public AuthenticationService(RestoratorDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SessionInfo>> SignInAsync(SignInDTO signIn)
        {
            var user = await _context.Users.Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Password == AccountPasswordHelper.HashUserPassword(signIn.Password) && u.Login == signIn.Login);

            if (user is null)
                return Result.Fail("Пользователь с такими данными не найден");

            var sessionInfo = new SessionInfo(user.Id, user.Username, user.Role.Name);

            return Result.Ok(sessionInfo);
        }
        public async Task<Result> SignUpAsync(SignUpDTO signUp)
        {
            if (_context.Users.Any(u => u.Login == signUp.Login))
                return Result.Fail("Такой логин занят");

            var user = new User()
            {
                Login = signUp.Login,
                Username = signUp.Username,
                Role = _context.Roles.FromEnum(Roles.User),
                Password = AccountPasswordHelper.HashUserPassword(signUp.Password),
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}