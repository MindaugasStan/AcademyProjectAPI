using System.ComponentModel.DataAnnotations;

namespace MouseTagProject.DTOs
{
    public class UserRegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmePassword { get; set; }
    }
}
