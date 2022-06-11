using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        [ForeignKey("Session")]
        public int? SessionId { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        [ForeignKey("Student")]
        public string StudentRollNumber { get; set; }
        public bool IsPresent { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual Session Session { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

    }
}