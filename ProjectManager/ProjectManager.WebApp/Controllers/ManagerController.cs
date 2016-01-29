using System;
using System.Linq;
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

        public ManagerController(ApplicationUserManager userManager, IAssignmentService assignmentService, IDictionaryService dictionaryService)
        {
            this._userManager = userManager;
            this._assignmentService = assignmentService;
            this._dictionaryService = dictionaryService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            var assigments = _assignmentService.GetAllByUserId(user.Id);
            var projects = user.Projects;

            var model = new DashboardViewModel
            {
                Assignments = assigments,
                User = user,

                BacklogCount = assigments.Count(x => x.Status.Type == StatusType.Backlog),
                DoneCount = assigments.Count(x => x.Status.Type == StatusType.Done),
                InProgressCount = assigments.Count(x => x.Status.Type == StatusType.InProgress),
                ReadyForReviewCount = assigments.Count(x => x.Status.Type == StatusType.ReadyForReview),
                ToDoCount = assigments.Count(x => x.Status.Type == StatusType.Todo),

                MinorCount = assigments.Count(x => x.Priority.Type == PriorityType.Minor),
                LowCount = assigments.Count(x => x.Priority.Type == PriorityType.Low),
                NormalCount = assigments.Count(x => x.Priority.Type == PriorityType.Normal),
                HighCount = assigments.Count(x => x.Priority.Type == PriorityType.High),
                CriticalCount = assigments.Count(x => x.Priority.Type == PriorityType.Criticial),

                TaskCount = assigments.Count(x => x.Category.Type == CategoryType.Task),
                BugCount = assigments.Count(x => x.Category.Type == CategoryType.Bug),
                ImprovmentCount = assigments.Count(x => x.Category.Type == CategoryType.Improvment),
                Projects = projects

            };
            return View(model);
        }

        [HttpGet]
        public ActionResult KanbanBoard()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());

            var usersInProject =
                _userManager.Users.Where(u => u.Projects.All(x => x.Id == user.ActiveProjectId));

            var Stasuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description");
            var Assignments = _assignmentService.GetAllByProjectId(user.ActiveProjectId);
            var Users = usersInProject.ToList();
            var ProjectId = user.ActiveProjectId ?? Guid.Empty;
            var model = new KanbanBoardViewModel
            {
                Stasuses = Stasuses,
                Assignments = Assignments,
                Users = Users,
                ProjectId = ProjectId

            };

            return View(model);
        }

        public FileContentResult GetAvatar()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());

            return File(user.Avatar, "image/jpg");
        }

        public ActionResult ChangeCurrentAssignment(string userId, string currentAssignmentId)
        {
            _assignmentService.ChangeTaskAssignment(userId, new Guid(currentAssignmentId));

            return Json(new { });
        }

        public ActionResult ChangeAssignmentStatus(string statusId, string currentAssignmentId)
        {
            _assignmentService.ChangeAssignmentStatus(int.Parse(statusId), new Guid(currentAssignmentId));

            return Json(new { });
        }
    }
}