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
        private readonly IAssignmentRepository _AssignmentRepository;

        public AssignmentService(IAssignmentRepository AssignmentRepository)
        {
            _AssignmentRepository = AssignmentRepository;
        }

        public IList<Assignment> GetAll()
        {
            return _AssignmentRepository.GetAll().ToList();
        }

        public IList<Assignment> GetAllByUserId(string id)
        {
            return _AssignmentRepository.GetAllByUserId(id).ToList();
        }

        public Assignment FindById(Guid id)
        {
            return _AssignmentRepository.FindById(id);
        }

        public void Add(Assignment Assignment)
        {
            _AssignmentRepository.Add(Assignment);
        }

        public void Update(Assignment Assignment)
        {
            _AssignmentRepository.Update(Assignment);
        }

        public bool Remove(Guid id)
        {
            return _AssignmentRepository.Remove(id);
        }

        public IList<Assignment> GetAllByAssignedToId(string id)
        {
            return _AssignmentRepository.GetAllByAssignedToId(id).ToList();
        }
    }
}
