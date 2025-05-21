using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models.Account;
using Restorator.Domain.Services;
using System.ComponentModel.DataAnnotations;
using AuthorizationResult = Restorator.Domain.Models.Account.AuthorizationResult;

namespace Restorator.API.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <inheritdoc/>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Запросить сброс пароля
        /// </summary>
        /// <param name="email">почта пользователя</param>
        [HttpPost("reset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RequestPasswordReset([FromBody][EmailAddress] string email)
        {
            var result = await _accountService.RequestPasswordReset(email);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }


        /// <summary>
        /// Востановление почты через код
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("recover")]
        [ProducesResponseType<AuthorizationResult>(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RecoverAccount(RecoverAccountDTO model)
        {
            var result = await _accountService.SignInAsync(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }


        /// <summary>
        /// Авторизоваться
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("signIn")]
        [ProducesResponseType<AuthorizationResult>(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            var result = await _accountService.SignInAsync(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }


        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("signUp")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
            var result = await _accountService.SignUpAsync(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }


        /// <summary>
        /// Сведения о аккаунте
        /// </summary>
        [Authorize]
        [HttpGet("info")]
        [ProducesResponseType<SessionInfo>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetSessionInfo()
        {
            var result = await _accountService.GetSessionInfoAsync();

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }


        /// <summary>
        /// Обновить пароль
        /// </summary>
        /// <param name="password"></param>
        [Authorize]
        [HttpPatch]
        [ProducesResponseType<SessionInfo>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdatePassword([FromBody] string password)
        {
            var result = await _accountService.UpdatePassword(password);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }
    }
}