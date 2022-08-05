using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register(UserRegisterDto user)
        {
            var result = await _userService.RegisterUserAsync(user);
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

        [HttpPost("Remove/{email}")]
        public async Task<IActionResult> RemoveUserAsync(string email)
        {
            var result = await _userService.RemoveUserAsync(email);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("usr"), Authorize(AuthenticationSchemes = "Bearer")]
        // [HttpGet("usr"), Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == "Id").Value;

            var user = await _userService.GetUserProfile(userId);

            return Ok(user);
        }


    }
}
