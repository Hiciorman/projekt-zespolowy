﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ApplicationUserManager _applicationUserManager;

        public ProjectsController(IProjectService projectService, ApplicationUserManager applicationUserManager)
        {
            _projectService = projectService;
            _applicationUserManager = applicationUserManager;
        }


        public ActionResult Activate(Guid id)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Profile");
            }

            var user = _applicationUserManager.FindById(userId);
            user.ActiveProjectId = id;
            _applicationUserManager.Update(user);

            return RedirectToAction("AllProjects", "Projects");
        }

        [HttpGet]
        public ActionResult AllProjects()
        {
            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return RedirectToAction("Login", "Profile");
            }

            var user = _applicationUserManager.FindById(userId);

            var model = new ProjectsViewModel
            {
                Projects = _projectService.GetAllByUserId(userId),
                ActiveProjectId = user.ActiveProjectId ?? Guid.Empty
            };

            return View(model);
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
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Profile");
            }

            var user = _applicationUserManager.FindById(userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Profile");
            }

            if (ModelState.IsValid)
            {
                _projectService.Add(project, userId);
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

        public JsonResult Delete(Guid id)
        {
            try
            {
                _projectService.Remove(id);
            }
            catch (Exception)
            {
                HttpNotFound();
                throw;
            }

            return Json(new { });
        }

        [HttpPost]
        public async Task<JsonResult> AddUser(string userName, string projectId)
        {
            var currentUser = await _applicationUserManager.FindByNameAsync(User.Identity.Name);
            var user = await _applicationUserManager.FindByNameAsync(userName);

            if (user != null && currentUser != null)
            {
                _projectService.AddMember(Guid.Parse(projectId), user.Id);

                return Json(new { result = "true" });
            }
            else
            {
                return Json(new { result = "false" });
            }
        }
    }
}
