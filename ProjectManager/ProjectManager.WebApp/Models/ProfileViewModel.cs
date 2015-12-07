using System.ComponentModel.DataAnnotations;

namespace ProjectManager.WebApp.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}