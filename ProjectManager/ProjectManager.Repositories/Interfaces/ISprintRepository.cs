using System;
using ProjectManager.Domain;

namespace ProjectManager.Repositories.Interfaces
{
    public interface ISprintRepository
    {
        void Add(Sprint sprint, Guid projectId);
        void Update(Sprint sprint);
        bool Remove(Guid sprintId);
        Sprint FindById(Guid sprintId);
    }
}
