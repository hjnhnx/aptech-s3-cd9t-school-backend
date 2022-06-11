using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class TermSubject
    {
        public int Id { get; set; }
        [ForeignKey("Term")]
        public int? TermId { get; set; }
        [ForeignKey("Subject")]
        public string SubjectCode { get; set; }
        public int SortNumber { get; set; }
        public virtual Term Term { get; set; }
        public virtual Subject Subject { get; set; }

    }
}