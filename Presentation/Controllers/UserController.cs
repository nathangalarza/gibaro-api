using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : MainController
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] RequestParameters requestParameters)
        {
            // TODO - Search by Username or Name
            var pagedResults = await _service.UserService.GetUsers(GetUserId(), requestParameters, false);
            return Ok(pagedResults.users);
        }

        [HttpGet("{id:guid}", Name = "UserById")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var users = await _service.UserService.GetUser(id, false);
            return Ok(users);
        }

        [HttpGet("LoggedIn")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            var users = await _service.UserService.GetUser(GetUserId(), false);
            return Ok(users);
        }

        [HttpGet("validate-username/{username}")]
        public async Task<IActionResult> ValidateUsernameExists(string username)
        {
            return Ok(await _service.UserService.ValidateUsernameExists(username, false));
        }

        [HttpGet("validate-email/{email}")]
        public async Task<IActionResult> ValidateEmailExists(string email)
        {
            var user = await _service.UserService.ValidateEmailExists(email, false);
            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _service.UserService.GetUserByUsername(GetUserId(), username, false);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto userCreateDto)
        {
            var result = await _service.UserService.CreateUser(userCreateDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    //ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserDeleteDto userDeleteDto)
        {
            await _service.UserService.DeleteUser(GetUserId(), userDeleteDto.Password!, false);
            return NoContent();
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            await _service.UserService.UpdateUser(GetUserId(), userUpdateDto, true);
            return NoContent();
        }

        [HttpDelete("deactivate")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeactivateUser([FromBody] UserDeleteDto userDeleteDto)
        {
            await _service.UserService.DeactivateUser(GetUserId(), userDeleteDto.Password!, true);
            return NoContent();
        }
    }
}
