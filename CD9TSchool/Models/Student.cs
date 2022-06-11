using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RollNumber { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Utils.Enum.Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? IdentityDateOfIssue { get; set; }
        public string IdentityPlaceOfIssue { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Email { get; set; }
        public string ParentFullName { get; set; }
        public string ParentAddress { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string ParentEmail { get; set; }
        public string ParentOccupation { get; set; }
        [ForeignKey("Group")]
        public string GroupName { get; set; }
        [ForeignKey("Program")]
        public int? ProgramId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual User User { get; set; }
        public virtual Program Program { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }
    }
}