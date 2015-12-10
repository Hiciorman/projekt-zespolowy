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
        void Add(Project project, string userId);
        void AddMember(Guid projectId, string userId);
        void Update(Project project);
        bool Remove(Guid id);
    }
}
