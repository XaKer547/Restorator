using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Helpers;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.Application.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestoratorDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IUserManager _userManager;
        public AccountService(RestoratorDbContext context,
                              IJwtService jwtService,
                              IUserManager userManager)
        {
            _context = context;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<Result<SessionInfo>> GetSessionInfoAsync()
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователя не существует");

            return new SessionInfo(user.Username, user.Role.Name);
        }
        public async Task<Result<AuthorizationResult>> SignInAsync(SignInDTO signIn)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Password == AccountPasswordHelper.HashUserPassword(signIn.Password)
                                           && u.Login == signIn.Login);

            if (user is null)
                return Result.Fail("Пользователь с такими данными не найден");

            var sessionInfo = new SessionInfo(user.Username, user.Role.Name);

            var result = new AuthorizationResult(sessionInfo, _jwtService.CreateToken(user.Id, user.Role.Name));

            return Result.Ok(result);
        }
        public async Task<Result> SignUpAsync(SignUpDTO signUp)
        {
            if (await _context.Users.AnyAsync(u => u.Login == signUp.Login))
                return Result.Fail("Такой логин занят");

            var user = new User()
            {
                Login = signUp.Login,
                Username = signUp.Username,
                Role = await _context.Roles.SingleAsync(r => r.Id == signUp.RoleId),
                Password = AccountPasswordHelper.HashUserPassword(signUp.Password),
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}