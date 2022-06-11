using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Course
    {
        public int Id { get; set; }
        [ForeignKey("Group")]
        public string GroupName { get; set; }
        [ForeignKey("Subject")]
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int SessionTotal { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        [ForeignKey("Slot")]
        public int? SlotId { get; set; }
        [ForeignKey("Term")]
        public int? TermId { get; set; }
        [ForeignKey("Room")]
        public string RoomNumber { get; set; }
        public Utils.Enum.CourseStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual Group Group { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Term Term { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }

    }
}