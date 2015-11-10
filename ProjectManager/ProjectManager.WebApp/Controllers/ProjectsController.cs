using System;
using System.Web.Mvc;
using ProjectManager.Domain;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IDictionaryService _dictionaryService;
        public ProjectsController(IProjectService projectService, IDictionaryService dictionaryService)
        {
            this._dictionaryService = dictionaryService;
            _projectService = projectService;
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

            return View(project);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Można wziąć po Identity.User cośtam - wskazuje aktualnie zalogowanego usera,
            //wtedy będzie można wyciągnąć jakieś dodatkowe rzeczy 
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Add(project);
                return RedirectToAction("AllProjects");
            }

            return View(project);
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
    }
}
