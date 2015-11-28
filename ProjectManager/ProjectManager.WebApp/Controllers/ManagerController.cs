using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectManager.Domain;
using ProjectManager.Services.Interfaces;
using ProjectManager.WebApp.Models;

namespace ProjectManager.WebApp.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IAssignmentService _assignmentService;
        private readonly IDictionaryService _dictionaryService;

        public ManagerController(ApplicationUserManager userManager ,IAssignmentService assignmentService, IDictionaryService dictionaryService)
        {
            this._userManager = userManager;
            this._assignmentService = assignmentService;
            this._dictionaryService = dictionaryService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KanbanBoard()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());

            var model = new KanbanBoardViewModel
            {
                Stasuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description"),
                Assignments = _assignmentService.GetAllByProjectId(user.ActiveProjectId)
            };

            return View(model);
        }

       
    }
}