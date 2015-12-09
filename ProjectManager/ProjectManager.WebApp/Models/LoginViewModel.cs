using System.ComponentModel.DataAnnotations;

namespace ProjectManager.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}