<<<<<<< HEAD
﻿using System;
using System.Reflection;
=======
﻿using System.Threading.Tasks;
>>>>>>> ed65d52bf42583568dcb82d068d3075ebac08d94
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
<<<<<<< HEAD
using Microsoft.Owin.Security.DataProtection;
=======
using ProjectManager.Helpers;
>>>>>>> ed65d52bf42583568dcb82d068d3075ebac08d94

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
            
           // var provider = new DpapiDataProtectionProvider("ProjectManager");

            //manager.UserTokenProvider =
               // new DataProtectorTokenProvider<User>(options.DataProtectionProvider);

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET"));
            }
            // manager.UserTokenProvider = new EmailTokenProvider<User, string>();
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