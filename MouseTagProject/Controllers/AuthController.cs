using Microsoft.AspNetCore.Authentication;
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


        [HttpGet, Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Aaaaas()
        {

            //    var ss = User.FindFirst(ClaimTypes.NameIdentifier);
            //     var asss = HttpContext.GetTokenAsync("access_token").Result;

            return Ok();
        }
    }
}
