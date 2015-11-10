using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectManager.Domain;
using ProjectManager.WebApp.Models;

namespace ProjectManager.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Details()
        {
            var model = new ProfileDetailsViewModel();

            var user = _userManager.FindById(User.Identity.GetUserId());

            if (user == null) return RedirectToAction("Start", "Main");

            model.Username = user.UserName;

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                //notification
                return RedirectToAction("Details", "Profile");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            //We should redirect to controller Manager action Dashboard
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Dashboard", "Manager");
                default:
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View(model);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User() { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true, true);
                return RedirectToAction("Start", "Main");
            }
            else
            {
                ModelState.AddModelError("failed", "Registration failed");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            _signInManager.AuthenticationManager.SignOut();

            return RedirectToAction("Start", "Main");
        }
    }
}