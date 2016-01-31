using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;

namespace ProjectManager.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly AppContext _context;

        public SprintRepository(AppContext context)
        {
            _context = context;
        }

        public void Add(Sprint sprint, Guid projectId)
        {
            _context.Projects.First(x => x.Id == projectId).Sprints.Add(sprint);
            _context.SaveChanges();
        }

        public void Update(Sprint sprint)
        {
            _context.Entry(sprint).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Remove(Guid sprintId)
        {
            try
            {
                Sprint sprint = FindById(sprintId);

                _context.Sprints.Remove(sprint);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Sprint FindById(Guid sprintId)
        {
            return _context.Sprints.First(x => x.Id == sprintId);
        }

        public IEnumerable<Sprint> SprintsInProject(Guid? projectId)
        {
            return _context.Projects.First(x => x.Id == projectId).Sprints;
        }

        public IEnumerable<Sprint> SprintsForUser(string userId)
        {
            var user = _context.Users.Include(a => a.Projects.Select(b => b.Sprints.Select(c => c.Assignemnts.Select(d => d.Status))))
                 .First(x => x.Id == userId.ToString());
            IList<Sprint> sprints = new List<Sprint>();

            foreach (var p in user.Projects)
            {
                foreach (var s in p.Sprints)
                {
                    sprints.Add(s);
                }
            }

            return sprints.Distinct();
        }
    }
}
