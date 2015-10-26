using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;

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

        public IList<Project> GetAllByUserId(string id)
        {
            return _projectRepository.GetAllByUserId(id).ToList();
        }

        public Project FindById(Guid id)
        {
            return _projectRepository.FindById(id);
        }

        public void Add(Project project)
        {
            _projectRepository.Add(project);
        }

        public void Update(Project project)
        {
            _projectRepository.Update(project);
        }

        public bool Remove(Guid id)
        {
            return _projectRepository.Remove(id);
        }
    }
}
