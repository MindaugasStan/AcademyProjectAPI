using System.ComponentModel.DataAnnotations;

namespace MouseTagProject.DTOs
{
    public class UserRegisterDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmePassword { get; set; }
        public string Role { get; set; }
    }
}
