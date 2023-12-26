using Project.CustomDataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        [UsernameCharacterSet]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(6)]
        [PasswordCharacterSet]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
