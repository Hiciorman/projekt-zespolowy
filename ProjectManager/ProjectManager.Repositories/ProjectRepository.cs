using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;

namespace ProjectManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppContext _context;
        private readonly ApplicationUserManager _userManager;

        public ProjectRepository(AppContext context, ApplicationUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.Include(x => x.Assignemnts);
        }

        public IEnumerable<Project> GetAllByUserId(string id)
        {
            return _context.Users
                .Where(x => x.Id == id)
                .SelectMany(x => x.Projects);
        }

        public IEnumerable<Project> GetAllUserProjectByUserId(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return _context.Projects.Where(x => x.Members.Contains(user));
        }

        public Project FindById(Guid id)
        {
            var x = _context.Projects.Where(b => b.Id == id).Include(p=>p.Members).First();
            return x;
        }

        public void Add(Project project, string userId)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == userId);
            project.Members.Add(user);

            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void AddMember(Guid projectId, string userId)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == userId);
            Project project = _context.Projects.FirstOrDefault(x => x.Id == projectId);

            if (project != null)
            {
                project.Members.Add(user);
                _context.SaveChanges();
            }
        }

        public void RemoveMember(Guid projectId, string userId)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == userId);
            Project project = _context.Projects.FirstOrDefault(x => x.Id == projectId);

            if (project != null)
            {
                project.Members.Remove(user);
                _context.SaveChanges();
            }
        }

        public void Update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Remove(Guid id)
        {
            try
            {
                Project project = FindById(id);

                _context.Assignments.RemoveRange(project.Assignemnts);
                _context.SaveChanges();

                _context.Sprints.RemoveRange(project.Sprints);
                _context.SaveChanges();

                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
       
    }
    
}
