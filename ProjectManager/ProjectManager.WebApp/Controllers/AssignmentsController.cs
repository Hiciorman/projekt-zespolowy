using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Controllers
{
    public class AssignmentsController : Controller
    {
        private AppContext db = new AppContext();

        // GET: Assignments
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.AssignedTo).Include(a => a.Category).Include(a => a.Owner).Include(a => a.Priority).Include(a => a.Project).Include(a => a.Status);
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);

            //TODO BOŻE CZEMU TYLE LOADÓW
            db.Entry(assignment).Reference(b => b.AssignedTo ).Load();
            db.Entry(assignment).Reference(b => b.Project).Load();
            db.Entry(assignment).Reference(b => b.Owner).Load();
            db.Entry(assignment).Reference(b => b.Priority).Load();
            db.Entry(assignment).Reference(b => b.Status).Load();
            db.Entry(assignment).Reference(b => b.Category).Load();
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "Email");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description");
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Description");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ProjectId,OwnerId,AssignedToId,StatusId,PriorityId,CategoryId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                assignment.Id = Guid.NewGuid();
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "Email", assignment.AssignedToId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", assignment.CategoryId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", assignment.OwnerId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Description", assignment.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignment.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description", assignment.StatusId);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "Email", assignment.AssignedToId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", assignment.CategoryId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", assignment.OwnerId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Description", assignment.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignment.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description", assignment.StatusId);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ProjectId,OwnerId,AssignedToId,StatusId,PriorityId,CategoryId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedToId = new SelectList(db.Users, "Id", "Email", assignment.AssignedToId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Description", assignment.CategoryId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", assignment.OwnerId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Description", assignment.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignment.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Description", assignment.StatusId);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
