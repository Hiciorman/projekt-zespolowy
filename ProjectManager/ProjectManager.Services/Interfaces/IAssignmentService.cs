using System;
using System.Collections.Generic;
using ProjectManager.Domain;

namespace ProjectManager.Services.Interfaces
{
    public interface IAssignmentService
    {
        IList<Assignment> GetAll();
        IList<Assignment> GetAllByUserId(string id);
        IList<Assignment> GetAllByProjectId(Guid? id);
        IList<Assignment> GetAllByDate(int year = 0, int month = 0, int day = 0);
        Assignment FindById(Guid id);
        void Add(Assignment Assignment);
        void Update(Assignment Assignment);
        bool Remove(Guid id);
    }
}
