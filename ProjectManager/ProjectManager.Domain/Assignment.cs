using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Domain
{
    public class Assignment : GuidEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //ustawiaw wartosc wskazujaca przez dataFormatString
        [DisplayName("Due date")]
        public DateTime? DueDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDateTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? StopDateTime { get; set; }
        [DataType(DataType.Time)]
        public  TimeSpan Estimation { get; set; }

        public Guid? ProjectId { get; set; }
        public Guid? SprintId { get; set; }
        public string OwnerId { get; set; }
        public string AssignedToId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [ForeignKey("SprintId")]
        public Sprint Sprint { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        [ForeignKey("AssignedToId")]
        public User AssignedTo { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}