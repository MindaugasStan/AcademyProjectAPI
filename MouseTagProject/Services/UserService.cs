using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MouseTagProject.DTOs;
using MouseTagProject.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MouseTagProject.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRegisterDto user)
        {
            var identityUser = new IdentityUser() { UserName = user.Email, Email = user.Email };

            //await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //await _roleManager.CreateAsync(new IdentityRole("User"));

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            await _userManager.AddToRoleAsync(identityUser, user.Role);

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

            var userRoles = await _userManager.GetRolesAsync(identityUser);

            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", identityUser.Id),
                new Claim("Email", identityUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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

        public async Task<bool> RemoveUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var userRemoved = await _userManager.DeleteAsync(user);

            if (userRemoved.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<IdentityUser> GetUserProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }
    }
}
