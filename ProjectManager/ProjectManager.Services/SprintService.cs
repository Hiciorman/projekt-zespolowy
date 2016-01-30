using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;

namespace ProjectManager.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        public SprintService(ISprintRepository sprintRepository, IAssignmentRepository assignmentRepository)
        {
            _sprintRepository = sprintRepository;
            _assignmentRepository = assignmentRepository;
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

        public Guid? GetNewestSprintId(Guid? projectId)
        {
            var list = _sprintRepository.GetAll().ToList();

            Sprint newestSprint = list.FirstOrDefault();

            foreach (var sprint in list)
            {
                if (sprint.StartDate <= DateTime.Now && sprint.EndDate >= DateTime.Now) newestSprint = sprint;
            }

            return newestSprint.Id;
        }

        public IList<Sprint> SprintsInProject(Guid? projectId)
        {
            var assignments = _assignmentRepository.GetAll().Where(x => x.ProjectId == projectId);

            List<Sprint> sprints = new List<Sprint>();

            foreach (var assignment in assignments)
            {
                if (assignment.ProjectId == projectId)
                    if (assignment.SprintId != null) sprints.Add(_sprintRepository.FindById(assignment.SprintId.Value));
            }

            return sprints.Distinct().ToList();
        }

    }
}
