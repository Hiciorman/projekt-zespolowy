using System;
using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.Services.Interfaces
{
    public interface IProjectService
    {
        IList<Project> GetAll();
        IList<Project> GetAllByUserId(string id);
        Project FindById(Guid id);
        void Add(Project project, string userId);
        void AddMember(Guid projectId, string userId);
        void Update(Project project);
        bool Remove(Guid id);
    }
}
