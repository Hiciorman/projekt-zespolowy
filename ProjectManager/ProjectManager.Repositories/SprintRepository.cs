using System;
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
            _context.Projects.First(x=> x.Id == projectId).Sprints.Add(sprint);
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

    }
}
