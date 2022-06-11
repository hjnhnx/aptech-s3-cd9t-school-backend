using System;

namespace CD9TSchool.Models.Dto
{
    public class TermDto
    {
        public int id { get; set; }
        public int termNumber { get; set; }
        public int? programId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string deletedBy { get; set; }
    }
}