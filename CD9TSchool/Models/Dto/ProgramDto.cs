using System;
using System.Linq;

namespace CD9TSchool.Models.Dto
{
    public class ProgramDto
    {
        public int id { get; set; }
        public string programCode { get; set; }
        public string programName { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string deletedBy { get; set; }
        public IQueryable<TermDto> terms { get; set; }
    }
}