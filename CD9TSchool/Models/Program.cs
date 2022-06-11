using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class Program
    {
        public int Id { get; set; }
        [Index(IsUnique=true)]
        [StringLength(100)]
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<Term> Terms { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Student> Students { get; set; }

    }
}