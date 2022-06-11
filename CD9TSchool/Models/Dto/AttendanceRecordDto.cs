namespace CD9TSchool.Models.Dto
{
    public class AttendanceRecordDto
    {
        public int sessionId { get; set; }
        public string studentRollNumber { get; set; }
        public bool isPresent { get; set; }
        public string comment { get; set; }
    }
}