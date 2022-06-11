using System.Linq;
using System.Web;
using System.Web.Http;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models.Dto;
using CD9TSchool.Utils;

namespace CD9TSchool.Controllers
{
    public class CourseController : ApiController
    {
        private readonly SchoolManager db = new SchoolManager();

        [HttpGet]
        [Route("api/courses-by-group")]
        [MyAuth(Enum.Role.STAFF)]
        public IHttpActionResult GetCourseByGroup(string groupName)
        {
            var courses = (from course in db.Courses
                where course.GroupName == groupName
                select new CourseDto
                {
                    id = course.Id,
                    groupName = course.GroupName,
                    subjectCode = course.SubjectCode,
                    subjectName = course.SubjectName
                }).ToList();

            return Ok(courses);
        }
        [HttpGet]
        [Route("api/student/courses")]
        [MyAuth(Enum.Role.STUDENT)]
        public IHttpActionResult GetCourseByStudent()
        {
            var dataUser = HttpContext.Current.User as MyPrincipal;
            var student = db.Students.FirstOrDefault(m => m.UserId == dataUser.Id);
            if (student == null || student.DeletedAt != null) return BadRequest();
            var courses = (from courseStudent in db.CourseStudents
                where courseStudent.StudentRollNumber == student.RollNumber
                select new CourseStudentDto()
                {
                    id = courseStudent.Course.Id,
                    subjectCode = courseStudent.Course.SubjectCode,
                    subjectName = courseStudent.Course.SubjectName,
                    sessionsTotal = courseStudent.Course.SessionTotal,
                    termId = courseStudent.Course.TermId,
                    termNumber = courseStudent.Course.Term.TermNumber,
                    startDate = courseStudent.Course.CreatedAt
                }).ToList();
            return Ok(courses);
        }

        [HttpGet]
        [Route("api/teacher/courses-by-group")]
        [MyAuth(Enum.Role.TEACHER)]
        public IHttpActionResult GetCourseByTeacher(string groupName)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var authTeacher = (from teacher in db.Teachers where teacher.UserId == authUser.Id select teacher).First();
            
            var courses = (from course in db.Courses
                where course.GroupName == groupName && course.TeacherId == authTeacher.Id
                select new CourseDto
                {
                    id = course.Id,
                    groupName = course.GroupName,
                    subjectCode = course.SubjectCode,
                    subjectName = course.SubjectName
                }).ToList();

            return Ok(courses);
        }
    }
}