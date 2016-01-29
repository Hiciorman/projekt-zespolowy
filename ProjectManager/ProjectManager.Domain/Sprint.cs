using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain
{
    public class Sprint : GuidEntity
    {
        public Sprint()
        {
            Assignemnts = new HashSet<Assignment>();
        }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int Type { get; set; }
        public virtual ICollection<Assignment> Assignemnts { get; set; }
    }
}
