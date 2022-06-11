using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public Utils.Enum.RoomType RoomType { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
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