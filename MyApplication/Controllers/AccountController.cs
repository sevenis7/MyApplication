using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApplicationServiceLayer.AccountService;
using MyApplicationServiceLayer.Authenticate;
using MyApplicationServiceLayer.Authenticate.Login.Models;
using MyApplicationServiceLayer.Authenticate.Registration.Models;
using MyApplicationServiceLayer.Tokens.RefreshTokenService;

namespace MyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AccountController(
            IAccountService accountService,
            IRefreshTokenService refreshTokenService)
        {
            _accountService = accountService;
            _refreshTokenService = refreshTokenService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code = "200">New user successfully created</response>
        /// <response code = "409">User already exists</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Register([FromBody] RegistrationModel model)
        {
            var result = await _accountService.Register(model);

            if (result.Succeeded)
                return Ok("User successfully created.");

            else
                return Conflict(string.Join(" ", result.Errors.Select(e => e.Description)));

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AuthenticatedResponse (access and refresh tokens)</returns>
        /// <response code = "200">Returns AuthenticatedResponse (access and refresh tokens)</response>
        /// <response code = "401">If user doesn't exists or wrong password</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AuthenticatedResponse>> Login([FromBody] LoginModel model)
        {
            var user = await _accountService.Login(model);

            if (user == null)
                return Unauthorized("There is no username with this password.");

            var response = await _accountService.Authenticate(user);

            return Ok(response);
        }

        /// <summary>
        /// Refresh tokens
        /// </summary>
        /// <param name="model"></param>
        /// <returns>AuthenticatedResponse (access and refresh tokens)</returns> 
        /// <response code = "200">Returns AuthenticatedResponse (access and refresh tokens)</response>
        /// <response code = "401">If user doesn't exists, wrong password or expired refresh token</response>
        /// <response code = "500">Internal Server Error</response>
        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AuthenticatedResponse>> Refresh([FromBody] RefreshModel model)
        {
            bool isValid = await _refreshTokenService.Validate(model.RefreshToken);

            if (!isValid)
                return Unauthorized();

            var user = await _refreshTokenService.GetUser(model.RefreshToken);

            if (user == null)
                return Unauthorized();

            await _refreshTokenService.Delete(model.RefreshToken);

            var response = await _accountService.Authenticate(user);

            return Ok(response);
        }

        /// <summary>
        /// Revoke refresh tokens
        /// </summary>
        /// <returns></returns>
        [HttpDelete("revoke")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Authorize]

        public async Task<ActionResult> Revoke()
        {
            var userName = HttpContext.User.Identity?.Name;

            if (userName is null)
                return Unauthorized();

            await _refreshTokenService.Revoke(userName);

            return Ok();
        }

    }
}
