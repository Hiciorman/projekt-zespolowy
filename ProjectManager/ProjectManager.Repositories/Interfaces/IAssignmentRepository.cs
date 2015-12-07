using ProjectManager.Domain;
using System;
using System.Collections.Generic;

namespace ProjectManager.Repositories.Interfaces
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAll();
        IEnumerable<Assignment> GetAllByUserId(string id);
        IEnumerable<Assignment> GetAllByAssignedToId(string id);
        IEnumerable<Assignment> GetAllByProjectId(Guid id);
        IEnumerable<Assignment> GetAllByDate(int year = 0, int month = 0, int day = 0);
        Assignment FindById(Guid id);
        void Add(Assignment Assignment);
        void Update(Assignment Assignment);
        bool Remove(Guid id);
        void ChangeTaskAssignment(string userId, Guid currentAssignmentId);
    }
}
