using Microsoft.AspNetCore.Identity;
using MouseTagProject.DTOs;

namespace MouseTagProject.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterUserAsync(UserRegisterDto user);
        Task<UserResponseDto> LoginUserAsync(UserLoginDto user);
        Task<IdentityUser> GetUserProfile(string id);
        Task<bool> RemoveUserAsync(string email);
    }
}
