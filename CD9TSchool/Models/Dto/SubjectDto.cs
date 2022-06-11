namespace CD9TSchool.Models.Dto
{
    public class SubjectDto
    {
        public string subjectCode { get; set; }
        public string subjectName { get; set; }
        public int sessionTotal { get; set; }

        public SubjectDto()
        {
        }

        public SubjectDto(Subject subject)
        {
            this.subjectCode = subject.SubjectCode;
            this.subjectName = subject.SubjectName;
            this.sessionTotal = subject.SessionTotal;
        }
    }
}