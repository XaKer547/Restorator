using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models.Account;
using Restorator.Domain.Models.Authorization;
using Restorator.Domain.Services;

namespace Restorator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] string email)
        {
            var result = await _accountService.RequestPasswordReset(email);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [HttpPost("recover")]
        public async Task<IActionResult> RecoverAccount(RecoverAccountDTO model)
        {
            var result = await _accountService.SignInAsync(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            var result = await _accountService.SignInAsync(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
            var result = await _accountService.SignUpAsync(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetSessionInfo()
        {
            var result = await _accountService.GetSessionInfoAsync();

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdatePassword([FromBody] string password)
        {
            var result = await _accountService.UpdatePassword(password);

            if (result.IsFailed)
                return BadRequest();

            return Ok();

        }
    }
}
