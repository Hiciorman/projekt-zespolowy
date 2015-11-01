using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ProjectManager.Domain
{
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}