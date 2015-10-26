using ProjectManager.Domain;
using System;
using System.Collections.Generic;

namespace ProjectManager.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetAllByUserId(string id);
        Project FindById(Guid id);
        void Add(Project project);
        void Update(Project project);
        bool Remove(Guid id);
    }
}
