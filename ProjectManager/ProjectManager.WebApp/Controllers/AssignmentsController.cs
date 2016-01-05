using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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
            var model = new CreateAssignmentViewModel
            {
                Assignment = new Assignment(),
                ListOfProjects = new SelectList(_projectService.GetAll(), "Id", "Name"),
                ListOfCategories = new SelectList(_dictionaryService.GetCategories(), "Id", "Description"),
                ListOfPriorities = new SelectList(_dictionaryService.GetPriorities(), "Id", "Description"),
                ListOfStatuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description"),
                ListOfUsers = new SelectList(_dictionaryService.GetUsers(), "Id", "UserName", User.Identity.GetUserId())
            };
            return View(model);
        }

        public ActionResult CreateFromProject(Guid id)
        {
            var model = new CreateAssignmentViewModel
            {
                Assignment = new Assignment(),
                ListOfProjects = new SelectList(_projectService.GetAll(), "Id", "Name", id),
                ListOfCategories = new SelectList(_dictionaryService.GetCategories(), "Id", "Description"),
                ListOfPriorities = new SelectList(_dictionaryService.GetPriorities(), "Id", "Description"),
                ListOfStatuses = new SelectList(_dictionaryService.GetStatuses(), "Id", "Description"),
                //  ListOfUsers = new SelectList(_projectService.FindById(id).Members, "Id", "UserName", User.Identity.GetUserId())
                ListOfUsers = new SelectList(_dictionaryService.GetUsers(), "Id", "UserName", User.Identity.GetUserId())
            };
            return View("Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Assignment assignment)
        {

            if (ModelState.IsValid)
            {
                _assignmentService.Add(assignment);
                return RedirectToAction("Index");
            }


            return View(assignment);
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

            Assignment assignment = _assignmentService.FindById(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }

            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _assignmentService.Update(assignment);
                return RedirectToAction("Index");
            }
            return View(assignment);
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
            return RedirectToAction("Index");
        }

    }
}
