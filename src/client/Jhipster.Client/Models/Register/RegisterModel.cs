using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Your username cannot be longer than 50 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Your password cannot be longer than 50 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Your confirmation password cannot be longer than 50 characters.")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Your email cannot be longer than 100 characters.")]
        public string Email { get; set; }
    }
}
