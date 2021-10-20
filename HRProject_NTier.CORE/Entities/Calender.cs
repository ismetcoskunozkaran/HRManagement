using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
   public class Calender
    {
        public int ID { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey("Manager")]
        public int ManagerID { get; set; }

        //Relations
        public Manager Manager { get; set; }
        public virtual ICollection<CalenderTopic> CalenderTopics { get; set; }
    }
}
