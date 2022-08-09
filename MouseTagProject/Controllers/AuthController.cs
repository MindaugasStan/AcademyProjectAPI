using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MouseTagProject.DTOs;
using MouseTagProject.Interfaces;
using System.Security.Claims;

namespace MouseTagProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if (result.IsSuccess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync(UserLoginDto user)
        {
            var result = await _userService.LoginUserAsync(user);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetIdentityUsers()
        {
            var identityUsers = _userService.GetIdentityUsers();
            List<IdentityUsersDto> users = new List<IdentityUsersDto>();
            identityUsers.ForEach(x => users.Add(new IdentityUsersDto() { UserName = x.UserName, Email = x.Email }));
            return Ok(users);
        }

        [HttpPost("Remove/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUserAsync(string email)
        {
            var result = await _userService.RemoveUserAsync(email);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("usrtest"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserProfile()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == "Id").Value;

            var user = await _userService.GetUserProfile(userId);

            return Ok(user);
        }


    }
}
