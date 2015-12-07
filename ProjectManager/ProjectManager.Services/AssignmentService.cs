using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public IList<Assignment> GetAll()
        {
            return _assignmentRepository.GetAll().ToList();
        }

        public IList<Assignment> GetAllByUserId(string id)
        {
            return _assignmentRepository.GetAllByUserId(id).ToList();
        }
        public IList<Assignment> GetAllByProjectId(Guid? id)
        {
            if (id == null)
            {
                return new List<Assignment>();
            }

            return _assignmentRepository.GetAllByProjectId((Guid)id).ToList();
        }

        public IList<Assignment> GetAllByDate(int year = 0, int month = 0, int day = 0)
        {
            return _assignmentRepository.GetAllByDate(year, month, day).ToList();
        }

        public Assignment FindById(Guid id)
        {
            return _assignmentRepository.FindById(id);
        }

        public void Add(Assignment Assignment)
        {
            _assignmentRepository.Add(Assignment);
        }

        public void Update(Assignment Assignment)
        {
            _assignmentRepository.Update(Assignment);
        }

        public bool Remove(Guid id)
        {
            return _assignmentRepository.Remove(id);
        }

        public void ChangeTaskAssignment(string userId, Guid currentAssignmentId)
        {
            _assignmentRepository.ChangeTaskAssignment(userId, currentAssignmentId);
        }

        public IList<Assignment> GetAllByAssignedToId(string id)
        {
            return _assignmentRepository.GetAllByAssignedToId(id).ToList();
        }
    }
}
