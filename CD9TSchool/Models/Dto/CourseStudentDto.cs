using System;

namespace CD9TSchool.Models.Dto
{
    public class CourseStudentDto
    {
        public int id { get; set; }
        public string subjectCode { get; set; }
        public string subjectName { get; set; }
        public int sessionsTotal { get; set; }
        public int? termId { get; set; }
        public int? termNumber { get; set; }
        public DateTime startDate { get; set; }
    }
}