using System;
using System.Threading.Tasks;
using System.Web.Management;
using System.Web.Mvc;
using ProjectManager.Domain;
using Microsoft.AspNet.Identity;
using ProjectManager.WebApp.Models;
using ProjectManager.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ProjectManager.WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IDictionaryService _dictionaryService;
        private readonly ApplicationUserManager _applicationUserManager;
        public ProjectsController(IProjectService projectService, IDictionaryService dictionaryService, ApplicationUserManager applicationUserManager)
        {
            this._dictionaryService = dictionaryService;
            _projectService = projectService;
            this._applicationUserManager = applicationUserManager;
        }

        [HttpGet]
        public ActionResult AllProjects()
        {
            var projects = _projectService.GetAll();
            return View(projects);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            Project project = _projectService.FindById(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            project.Owner = _applicationUserManager.FindById(project.OwnerId);
            foreach (var assignment in project.Assignemnts)
            {
                assignment.AssignedTo = _applicationUserManager.FindById(assignment.AssignedToId);
                assignment.Category = _dictionaryService.GetCategories().First(c => c.Id == assignment.CategoryId);
                assignment.Owner = _applicationUserManager.FindById(assignment.OwnerId);
                assignment.Priority = _dictionaryService.GetPriorities().First(p=> p.Id ==assignment.PriorityId);
                assignment.Status = _dictionaryService.GetStatuses().First(s => s.Id == assignment.StatusId);
            }
            return View(project);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.OwnerId = User.Identity.GetUserId();
                
                _projectService.Add(project);
                return RedirectToAction("AllProjects");
            }

            return View();
        }

        public ActionResult Edit(Guid id)
        {
            Project project = _projectService.FindById(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Update(project);
                return RedirectToAction("AllProjects");
            }

            return View(project);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Project project = _projectService.FindById(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _projectService.Remove(id);

            return RedirectToAction("AllProjects");
        }

        [HttpPost]
        public async Task<JsonResult> AddUser(string userName, string projectId)
        {
            var currentUser = await _applicationUserManager.FindByNameAsync(User.Identity.Name);

            //get user to add to project
            var user = await _applicationUserManager.FindByNameAsync(userName);

            //if not found or user not logged return false
            if (user != null && currentUser != null)
            {
                //TODO: find way to finish this

                return Json(new { result = "true"});
            }
            else
            {
                return Json(new { result = "false" });
            }
        }
    }
}
