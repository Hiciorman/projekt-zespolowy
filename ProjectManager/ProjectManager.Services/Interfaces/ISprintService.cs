using System;
using ProjectManager.Domain;

namespace ProjectManager.Services.Interfaces
{
    public interface ISprintService
    {
        void Add(Sprint sprint, Guid projectId);
        void Update(Sprint sprint);
        bool Remove(Guid sprintId);
        Sprint FindById(Guid sprintId);
    }
}
