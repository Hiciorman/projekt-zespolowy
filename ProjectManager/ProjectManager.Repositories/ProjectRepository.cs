﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;

namespace ProjectManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppContext _context;
        private readonly ApplicationUserManager _applicationUserManager;

        public ProjectRepository(AppContext context, ApplicationUserManager applicationUserManager)
        {
            _context = context;
            this._applicationUserManager = applicationUserManager;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.Include(x => x.Assignemnts);
        }

        public IEnumerable<Project> GetAllByUserId(string id)
        {
            return _context.Projects.Where(x => x.OwnerId == id);
        }

        public IEnumerable<Project> GetAllUserProjectByUserId(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return _context.Projects.Where(x => x.Members.Contains(user));
        }

        public Project FindById(Guid id)
        {
            var x = _context.Projects.Where(b => b.Id == id).Include(p=>p.Members).First();
            return x;
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Remove(Guid id)
        {
            try
            {
                Project project = FindById(id);

                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
       
    }
    
}
