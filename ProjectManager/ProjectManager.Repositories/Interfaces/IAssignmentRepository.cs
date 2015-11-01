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
        Assignment FindById(Guid id);
        void Add(Assignment Assignment);
        void Update(Assignment Assignment);
        bool Remove(Guid id);
    }
}
