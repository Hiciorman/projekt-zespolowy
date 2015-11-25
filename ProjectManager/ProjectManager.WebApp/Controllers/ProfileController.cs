using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectManager.Domain;
using ProjectManager.WebApp.Models;
using ProjectManager.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAssignmentService _assignmentService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public ProfileController(IProjectService projectService, IAssignmentService assignmentService, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this._projectService = projectService;
            this._assignmentService = assignmentService;
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

                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

                string callbackUrl = Url.Action("ConfirmEmail",
                    "Profile",
                    new { userId = user.Id, code = code },
                    protocol: Request.Url.Scheme
                    );

                await
                    _userManager.SendEmailAsync(user.Id, "Potwierdzenie konta",
                        "Aby potwierdzić email kliknij <a href=\""
                        + callbackUrl + "\">here</a>");

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
        [HttpGet]
        public ActionResult Assignments()
        {
            var projects = _projectService.GetAllByUserId(User.Identity.GetUserId()).ToList();
            var assignments = new List<List<Assignment>>(projects.Count);
            for (int i = 0; i < projects.Count; i++)
            {
                assignments.Add(new List<Assignment>());
                assignments[i] = _assignmentService.GetAllByProjectId(projects[i].Id).Where(a => a.AssignedToId == User.Identity.GetUserId()).ToList();
            }

            var model = new UserAssignmentsViewModel
            {
                Projects = projects,
                Assignments = assignments

            };

            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}