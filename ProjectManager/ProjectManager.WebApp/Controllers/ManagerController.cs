using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly ISprintService _sprintService;
        private readonly IProjectService _projectService;

        public ManagerController(ApplicationUserManager userManager, IAssignmentService assignmentService, IDictionaryService dictionaryService, ISprintService sprintService, IProjectService projectService)
        {
            this._userManager = userManager;
            this._assignmentService = assignmentService;
            this._dictionaryService = dictionaryService;
            this._sprintService = sprintService;
            this._projectService = projectService;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            var assigments = _assignmentService.GetAllByUserId(user.Id);
            var sprints = _sprintService.SprintsForUser(user.Id);
            var sprintTuple = new List<Tuple<string, float, float, float, float, float>> { };//name, to do, inprogress, readyforreview, done, all

            foreach (var s in sprints)
            {
                var todoList = s.Assignemnts.Where(x => x.Status.Type == StatusType.Todo);
                var inProgressList = s.Assignemnts.Where(x => x.Status.Type == StatusType.InProgress);
                var readyForReviewList = s.Assignemnts.Where(x => x.Status.Type == StatusType.ReadyForReview);
                var doneList = s.Assignemnts.Where(x => x.Status.Type == StatusType.Done);
                var allCount = s.Assignemnts.Count(x => x.Status.Type != StatusType.Backlog);
                sprintTuple.Add(new Tuple<string, float, float, float, float, float>(
                    s.Name,
                    todoList.Count(),
                    inProgressList.Count(),
                    readyForReviewList.Count(),
                    doneList.Count(),
                    allCount
                    ));
            }


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
                Sprints = sprintTuple,
                Projects = user.Projects

            };
            return View(model);
        }

        [HttpGet]
        public ActionResult KanbanBoard(KanbanBoardViewModel kanbanModel)
        {
            var user = _userManager.FindById(User.Identity.GetUserId());

            var Stasuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description");
            var Users = _projectService.GetUsersInProject(user.ActiveProjectId);
            var ProjectId = user.ActiveProjectId ?? Guid.Empty;

            string CurrentSprint;
            if (String.IsNullOrEmpty(kanbanModel.CurrentSprint))
                CurrentSprint = _sprintService.GetNewestSprintId(user.ActiveProjectId).ToString();
            else CurrentSprint = kanbanModel.CurrentSprint;

            var Assignments = _assignmentService.GetAllByProjectIdAndSprint(user.ActiveProjectId, new Guid(CurrentSprint));
            var SprintsInProject = _sprintService.SprintsInProject(user.ActiveProjectId);

            var model = new KanbanBoardViewModel
            {
                Stasuses = Stasuses,
                Assignments = Assignments,
                Users = Users,
                ProjectId = ProjectId,
                CurrentSprint = CurrentSprint,
                AllSprints = new List<SelectListItem>()
            };

            foreach (var sprint in SprintsInProject)
            {
                var item = new SelectListItem();
                item.Text = sprint.Name;
                item.Value = sprint.Id.ToString();
                if (sprint.Id == new Guid(CurrentSprint))
                {
                    item.Selected = true;
                }
                model.AllSprints.Add(item);
            }

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