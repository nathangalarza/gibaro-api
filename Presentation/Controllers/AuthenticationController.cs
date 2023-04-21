using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto user)
        {
            return Ok(await _service.AuthenticationService.AuthenticateUser(user));
        }

        [HttpPost("refresh-token")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await
            _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }

        //[HttpGet("verify-email")]
        //public async Task<IActionResult> VerifyEmail(string userId, string token, string? email)
        //{
        //    await _service.AuthenticationService.VerifyEmail(userId, token, email);
        //    return NoContent();
        //}

        //[HttpGet("verify-phone")]
        //public async Task<IActionResult> VerifyPhone(string userId, string token)
        //{
        //    await _service.AuthenticationService.VerifyPhoneNumber(userId, token);
        //    return NoContent();
        //}
    }
}
