using System;

namespace CD9TSchool.Models.Dto
{
    public class AttendanceReportDto
    {
        public int id { get; set; }
        public int? sessionId { get; set; }
        public int? courseId { get; set; }
        public string studentRollNumber { get; set; }
        public bool isPresent { get; set; }
        public string comment { get; set; }
        public string studentFullName { get; set; }
        public DateTime date  { get; set; }
        
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string subjectCode  { get; set; }
        public string roomNumber  { get; set; }
    }
}