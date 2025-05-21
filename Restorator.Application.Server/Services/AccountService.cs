using FluentResults;
using Microsoft.EntityFrameworkCore;
using Restorator.DataAccess.Data;
using Restorator.DataAccess.Data.Entities;
using Restorator.DataAccess.Helpers;
using Restorator.Domain.Models.Account;
using Restorator.Domain.Services;
using Restorator.Mail.Models.Templates;
using Restorator.Mail.Services;

namespace Restorator.Application.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestoratorDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IUserManager _userManager;
        private readonly IMailService _mailService;

        public AccountService(RestoratorDbContext context,
                              IJwtService jwtService,
                              IUserManager userManager,
                              IMailService mailService)
        {
            _context = context;
            _jwtService = jwtService;
            _userManager = userManager;
            _mailService = mailService;
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
        public async Task<Result<AuthorizationResult>> SignInAsync(SignInDTO model)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Password == AccountPasswordHelper.HashUserPassword(model.Password)
                                           && u.Login == model.Login);

            if (user is null)
                return Result.Fail("Пользователь с такими данными не найден");

            var sessionInfo = new SessionInfo(user.Username, user.Role.Name);

            var result = new AuthorizationResult(sessionInfo, _jwtService.CreateToken(user.Id, user.Role.Name));

            return Result.Ok(result);
        }
        public async Task<Result> SignUpAsync(SignUpDTO model)
        {
            if (await _context.Users.AnyAsync(u => u.Login == model.Login))
                return Result.Fail("Такой логин занят");

            var user = new User()
            {
                Login = model.Login,
                Username = model.Username,
                Email = model.Email,
                Role = await _context.Roles.SingleAsync(r => r.Id == model.RoleId),
                Password = AccountPasswordHelper.HashUserPassword(model.Password),
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<Result> RequestPasswordReset(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user is null)
                return Result.Fail("Пользователя не существует");

            var code = AccountPasswordHelper.GenereateOtpCode();

            user.OTP = code;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            await _mailService.SendMailAsync(new PasswordRecoveryMailTemplate(user.Email, user.Username, user.OTP));

            return Result.Ok();
        }
        public async Task<Result<AuthorizationResult>> SignInAsync(RecoverAccountDTO model)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.OTP == model.OTP && u.Email == model.Email);

            if (user is null)
                return Result.Fail("Пользователь с такими данными не найден");

            var sessionInfo = new SessionInfo(user.Username, user.Role.Name);

            var result = new AuthorizationResult(sessionInfo, _jwtService.CreateToken(user.Id, user.Role.Name));

            user.OTP = null;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Result.Ok(result);
        }
        public async Task<Result> UpdatePassword(string password)
        {
            if (!_userManager.TryGetUserId(out var userId))
                return Result.Fail("Не удалось получить id пользователя");

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return Result.Fail("Пользователя не существует");

            user.Password = AccountPasswordHelper.HashUserPassword(password);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}