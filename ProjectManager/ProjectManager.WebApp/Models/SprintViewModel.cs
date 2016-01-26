using System;
using ProjectManager.Domain;

namespace ProjectManager.WebApp.Models
{
    public class SprintViewModel
    {
        public Guid ProjectId { get; set; }
        public Sprint Sprint { get; set; }
    }
}