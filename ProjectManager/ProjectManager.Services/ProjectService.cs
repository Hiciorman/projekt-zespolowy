using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IList<Project> GetAll()
        {
            return _projectRepository.GetAll().ToList();
        }
    }
}
