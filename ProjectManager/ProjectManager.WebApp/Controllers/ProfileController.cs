using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectManager.Domain;
using ProjectManager.WebApp.Models;
using ProjectManager.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

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
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("Start", "Main");
            }

            var model = new ProfileViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("Start", "Main");
            }

            var model = new ProfileViewModel()
            {
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProfileViewModel model, HttpPostedFileBase upload)
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("Start", "Main");
            }

            if (upload != null && upload.ContentLength > 0)
            {
                WebImage img = new WebImage(upload.InputStream);
                if (img.Width > 32)
                    img.Resize(32, 32);
                user.Avatar = img.GetBytes();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.Username;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Details", "Profile");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Profile");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
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
                case SignInStatus.Failure:
                    ModelState.AddModelError(string.Empty, "Incorrect username or password");
                    break;
                case SignInStatus.RequiresVerification:
                    ModelState.AddModelError(string.Empty, "Account requires verification");
                    break;
                case SignInStatus.LockedOut:
                    ModelState.AddModelError(string.Empty, "Account is locked out");
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Try login again after few minutes");
                    break;
            }

            return View(model);
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

            var user = new User() { UserName = model.Username, Email = model.EmailAdress };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true, true);
                return RedirectToAction("Start", "Main");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
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
            //TODO: Zrobić lepiej [KD]
            var projects = _projectService.GetAll().ToList();
            var assignmentsAssignedTo = new List<List<Assignment>>(projects.Count);
            var assignmentsOwnedBy = new List<List<Assignment>>(projects.Count);

            for (int i = 0; i < projects.Count; i++)
            {
                assignmentsAssignedTo.Add(new List<Assignment>());
                assignmentsAssignedTo[i] = _assignmentService.GetAllByProjectId(projects[i].Id).Where(a => a.AssignedToId == User.Identity.GetUserId()).ToList();
            }
             for (int i = 0; i < projects.Count; i++)
            {
                assignmentsOwnedBy.Add(new List<Assignment>());
                assignmentsOwnedBy[i] = _assignmentService.GetAllByProjectId(projects[i].Id).Where(a => a.OwnerId == User.Identity.GetUserId()).ToList();
            }

            var model = new UserAssignmentsViewModel
            {
                Projects = projects,
                AssignmentsAssignedTo = assignmentsAssignedTo,
                AssignmentsOwnedBy = assignmentsOwnedBy

            };

            return View(model);
        }
    }
}