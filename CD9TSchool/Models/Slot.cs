using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupNameSuffix { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

    }
}