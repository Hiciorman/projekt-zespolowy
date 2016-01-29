using System;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;

        public SprintService(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public void Add(Sprint sprint, Guid projectId)
        {
            _sprintRepository.Add(sprint, projectId);
        }

        public void Update(Sprint sprint)
        {
            _sprintRepository.Update(sprint);
        }

        public bool Remove(Guid sprintId)
        {
            return _sprintRepository.Remove(sprintId);
        }

        public Sprint FindById(Guid sprintId)
        {
            return _sprintRepository.FindById(sprintId);
        }
    }
}
