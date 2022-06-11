using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string GroupName { get; set; }
        public DateTime StartDate { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        [ForeignKey("Slot")]
        public int? SlotId { get; set; }
        [ForeignKey("Program")]
        public int? ProgramId { get; set; }
        [ForeignKey("Term")]
        public int? TermId { get; set; }
        public string DaysOfWeek { get; set; }
        [ForeignKey("Room")]
        public string RoomNumber { get; set; }
        public bool HasGeneratedTimetable { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Program Program { get; set; }
        public virtual Room Room { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Group()
        {
            HasGeneratedTimetable = false;
        }
    }
}