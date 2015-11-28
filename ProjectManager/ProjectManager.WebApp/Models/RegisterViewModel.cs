using System.ComponentModel.DataAnnotations;

namespace ProjectManager.WebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Enter valid email adress")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string PasswordAgain { get; set; }
    }
}