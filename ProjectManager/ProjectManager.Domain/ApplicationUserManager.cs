using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProjectManager.Helpers;

namespace ProjectManager.Domain
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {

            var manager = new ApplicationUserManager(
                new UserStore<User>(context.Get<AppContext>()));

            // Configure validation logic for usernames
            manager.UserValidator =
                new UserValidator<User>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            return manager;
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            var generator = new AvatarGenerator();
            user.Avatar = generator.Generate(user.UserName);

            return base.CreateAsync(user, password);
        }
    }
}