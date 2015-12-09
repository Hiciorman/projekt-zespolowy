using System.ComponentModel.DataAnnotations;

namespace ProjectManager.WebApp.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string NewPassword { get; set; }
    }
}