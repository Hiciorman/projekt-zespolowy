using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Domain
{
    public class Assignment : GuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid? ProjectId { get; set; }
        public string OwnerId { get; set; }
        public string AssignedToId { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        [ForeignKey("AssignedToId")]
        public User AssignedTo { get; set; }

        //TODO: Add Category and Status

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}