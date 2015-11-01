using System;
using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.Services.Interfaces
{
    public interface IAssignmentService
    {
        IList<Assignment> GetAll();
        IList<Assignment> GetAllByUserId(string id);
        Assignment FindById(Guid id);
        void Add(Assignment Assignment);
        void Update(Assignment Assignment);
        bool Remove(Guid id);
    }
}
