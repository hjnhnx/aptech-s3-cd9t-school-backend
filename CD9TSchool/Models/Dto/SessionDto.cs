using System;

namespace CD9TSchool.Controllers
{
    public class SessionDto
    {
        public int id { get; set; }
        public string groupName { get; set; }
        public string subjectCode { get; set; }
        public string subjectName { get; set; }
        public string teacherName { get; set; }
        public DateTime date { get; set; }
        public string roomNumber { get; set; }
        public string cancelReason { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public int? teacherId { get; set; }
        public int? slotId { get; set; }
        public int? courseId { get; set; }
        public Utils.Enum.SessionStatus status { get; set; }
    }
}