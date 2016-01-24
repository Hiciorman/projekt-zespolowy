using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProjectManager.Domain;
using Microsoft.AspNet.Identity;
using ProjectManager.Services.Interfaces;
using ProjectManager.WebApp.Models;

namespace ProjectManager.WebApp.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAssignmentService _assignmentService;
        private readonly IDictionaryService _dictionaryService;
        public AssignmentsController(IProjectService projectService, IAssignmentService assignmentService, IDictionaryService dictionaryService)
        {
            _assignmentService = assignmentService;
            this._dictionaryService = dictionaryService;
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            var assignments = _assignmentService.GetAll();
            return View(assignments.ToList());
        }

        public ActionResult Details(Guid id)
        {

            Assignment assignment = _assignmentService.FindById(id);


            if (assignment == null)
            {
                return HttpNotFound();
            }

            return View(assignment);
        }

        public ActionResult Create()
        {
            var projects = _projectService.GetAllByUserId(User.Identity.GetUserId());
            if (!projects.Any())
            {
                //Przydałyby się jakieś notyfikacje 
                return RedirectToAction("Dashboard", "Manager");
            }

            var model = new CreateAssignmentViewModel
            {
                Assignment = new Assignment(),
                ListOfProjects = new SelectList(projects, "Id", "Name"),
                ListOfCategories = new SelectList(_dictionaryService.GetCategories(), "Id", "Description"),
                ListOfPriorities = new SelectList(_dictionaryService.GetPriorities(), "Id", "Description"),
                ListOfStatuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description"),
                ListOfUsers =
                    new SelectList(_dictionaryService.GetUsers(projects.FirstOrDefault().Id), "Id", "UserName",
                        User.Identity.GetUserId())
            };
            return View(model);
        }

        public ActionResult CreateFromProject(Guid id)
        {
            var projects = _projectService.GetAllByUserId(User.Identity.GetUserId());
            var model = new CreateAssignmentViewModel
            {
                Assignment = new Assignment(),
                ListOfProjects = new SelectList(projects, "Id", "Name", id),
                ListOfCategories = new SelectList(_dictionaryService.GetCategories(), "Id", "Description"),
                ListOfPriorities = new SelectList(_dictionaryService.GetPriorities(), "Id", "Description"),
                ListOfStatuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description"),
                ListOfUsers = new SelectList(_dictionaryService.GetUsers(id), "Id", "UserName", User.Identity.GetUserId())
            };
            return View("Create", model);
        }

        [HttpPost]
        public async Task<JsonResult> ChangeProject(string id)
        {
            if (id != null)
            {
                var listOfUsers = new SelectList(_dictionaryService.GetUsers(Guid.Parse(id)), "Id", "UserName",
                    User.Identity.GetUserId());

                return Json(new { result = listOfUsers });
            }
            else
            {
                return Json(new { result = "false" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Assignment assignment)
        {

            if (ModelState.IsValid)
            {
                _assignmentService.Add(assignment);
                return RedirectToAction("Details", "Assignments", new { id = assignment.Id });
            }

            return RedirectToAction("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromProject(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _assignmentService.Add(assignment);
                return RedirectToAction("Details", "Projects", new { id = assignment.ProjectId });
            }

            return View(assignment);
        }

        public ActionResult Edit(Guid id)
        {
            var projects = _projectService.GetAllByUserId(User.Identity.GetUserId());
            if (!projects.Any())
            {
                return RedirectToAction("Dashboard", "Manager");
            }

            Assignment assignment = _assignmentService.FindById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            var model = new EditAssignmentViewModel
            {
                Assignment = assignment,
                ListOfProjects = new SelectList(projects, "Id", "Name"),
                ListOfCategories = new SelectList(_dictionaryService.GetCategories(), "Id", "Description"),
                ListOfPriorities = new SelectList(_dictionaryService.GetPriorities(), "Id", "Description"),
                ListOfStatuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description"),
                ListOfUsers =
                    new SelectList(_dictionaryService.GetUsers(projects.FirstOrDefault().Id), "Id", "UserName",
                        User.Identity.GetUserId())
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _assignmentService.Update(model.Assignment);
                return RedirectToAction("Details", "Assignments", new { id = model.Assignment.Id });
            }

            return RedirectToAction("Edit", new { id = model.Assignment.Id });
        }

        public ActionResult Delete(Guid id)
        {
            Assignment assignment = _assignmentService.FindById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _assignmentService.Remove(id);
            return RedirectToAction("Dashboard", "Manager");
        }
    }
}
