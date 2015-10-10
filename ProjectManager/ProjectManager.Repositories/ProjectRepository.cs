using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace ProjectManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppContext _context;

        public ProjectRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.Include(x => x.Assignemnts);
        }
    }
}
