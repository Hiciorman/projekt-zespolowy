using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;

namespace ProjectManager.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public readonly AppContext _context;

        public AssignmentRepository(AppContext context)
        {
            _context = context;
        }

        public IEnumerable<Assignment> GetAll()
        {
            return _context.Assignments.LoadRelatedEntities();
        }

        public IEnumerable<Assignment> GetAllByUserId(string id)
        {
            return _context.Assignments.Where(x => x.OwnerId == id).LoadRelatedEntities(); ;
        }

        public IEnumerable<Assignment> GetAllByAssignedToId(string id)
        {
            return _context.Assignments.Where(x => x.AssignedToId == id).LoadRelatedEntities(); ;
        }

        public IEnumerable<Assignment> GetAllByProjectId(Guid id)
        {
            var a = _context.Assignments.Where(x => x.ProjectId == id).LoadRelatedEntities();
            return a;
        }

        public IEnumerable<Assignment> GetAllByDate(int year = 0, int month = 0, int day = 0)
        {
            var result = _context.Assignments.Where(x => x.DueDate != null);
            if (year != 0)
            {
                result = result.Where(x => x.DueDate.GetValueOrDefault().Year == year);
            }
            if (month != 0)
            {
                result = result.Where(x => x.DueDate.GetValueOrDefault().Month == month);
            }
            if (day != 0)
            {
                result = result.Where(x => x.DueDate.GetValueOrDefault().Day == day);
            }
            return result.LoadRelatedEntities();
        }

        public Assignment FindById(Guid id)
        {
            var assignment = _context.Assignments.FirstOrDefault(x => x.Id == id);
            _context.Entry(assignment).Reference(b => b.AssignedTo).Load();
            _context.Entry(assignment).Reference(b => b.Category).Load();
            _context.Entry(assignment).Reference(b => b.Owner).Load();
            _context.Entry(assignment).Reference(b => b.Priority).Load();
            _context.Entry(assignment).Reference(b => b.Status).Load();
            _context.Entry(assignment).Reference(b => b.Project).Load();
            return assignment;
        }

        public void Add(Assignment Assignment)
        {
            _context.Assignments.Add(Assignment);
            _context.SaveChanges();
        }

        public void Update(Assignment Assignment)
        {
            _context.Entry(Assignment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Remove(Guid id)
        {
            try
            {
                Assignment Assignment = FindById(id);

                _context.Assignments.Remove(Assignment);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void ChangeTaskAssignment(string userId, Guid currentAssignmentId)
        {
            try
            {
                var assignment = FindById(currentAssignmentId);

                if (String.IsNullOrEmpty(userId))
                {
                    assignment.AssignedToId = null;
                }
                else
                {
                    assignment.AssignedToId = userId;
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ChangeAssignmentStatus(int statusId, Guid currentAssignmentId)
        {
            try
            {
                var assignment = FindById(currentAssignmentId);

                var status = _context.Statuses.ToList().Find(x => x.Id == statusId);

                assignment.Status = status;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    static class Extension
    {
        public static IQueryable<Assignment> LoadRelatedEntities(this IQueryable<Assignment> query)
        {
            return query.Include(b => b.AssignedTo).
                        Include(b => b.Category).
                        Include(b => b.Owner).
                        Include(b => b.Priority).
                        Include(b => b.Project).
                        Include(b => b.Status);
        }


    }
}
