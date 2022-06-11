using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Session
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Slot")]
        public int? SlotId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherId { get; set; }
        [ForeignKey("Room")]
        public string RoomNumber { get; set; }
        public Utils.Enum.SessionStatus Status { get; set; }
        public string CancelReason { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Room Room { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }

    }
}