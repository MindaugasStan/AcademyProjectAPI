using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MouseTagProject.DTOs;
using MouseTagProject.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MouseTagProject.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRegisterDto user)
        {
            var identityUser = new IdentityUser() { UserName = user.Email, Email = user.Email, };
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded)
            {
                return new UserResponseDto()
                {
                    Message = "User created successfully.",
                    IsSuccess = true,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new UserResponseDto()
            {
                Message = "User did not created.",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserResponseDto> LoginUserAsync(UserLoginDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser == null)
            {
                return new UserResponseDto()
                {
                    Message = "User did not found.",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.CheckPasswordAsync(identityUser, user.Password);

            if (!result)
            {
                return new UserResponseDto()
                {
                    Message = "Invalid password.",
                    IsSuccess = false,
                };
            }

            var claims = new[]
            {
                new Claim("Id", identityUser.Id),
                new Claim("Email", identityUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["JwtConfig:Issuer"],
                audience: _config["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserResponseDto()
            {
                Message = tokenString,
                IsSuccess = true,
                ExpiredDate = token.ValidTo
            };
        }

        public async Task<IdentityUser> GetUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }
    }
}
