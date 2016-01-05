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

                BacklogCount = assigments.Where(x => x.Status.Type == StatusType.Backlog).Count(),
                DoneCount = assigments.Where(x => x.Status.Type == StatusType.Done).Count(),
                InProgressCount = assigments.Where(x => x.Status.Type == StatusType.InProgress).Count(),
                ReadyForReviewCount = assigments.Where(x => x.Status.Type == StatusType.ReadyForReview).Count(),
                ToDoCount = assigments.Where(x => x.Status.Type == StatusType.Todo).Count(),

                MinorCount = assigments.Where(x => x.Priority.Type == PriorityType.Minor).Count(),
                LowCount = assigments.Where(x => x.Priority.Type == PriorityType.Low).Count(),
                NormalCount = assigments.Where(x => x.Priority.Type == PriorityType.Normal).Count(),
                HighCount = assigments.Where(x => x.Priority.Type == PriorityType.High).Count(),
                CriticalCount = assigments.Where(x => x.Priority.Type == PriorityType.Criticial).Count(),

                TaskCount = assigments.Where(x => x.Category.Type == CategoryType.Task).Count(),
                BugCount = assigments.Where(x => x.Category.Type == CategoryType.Bug).Count(),
                ImprovmentCount = assigments.Where(x => x.Category.Type == CategoryType.Improvment).Count(),
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
            var ProjectId = user.ActiveProjectId.Value;
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