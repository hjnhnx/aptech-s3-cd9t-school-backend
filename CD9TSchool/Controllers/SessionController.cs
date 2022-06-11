using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models.Dto;
using Enum = CD9TSchool.Utils.Enum;

namespace CD9TSchool.Controllers
{
    public class SessionController : ApiController
    {
        private SchoolManager db = new SchoolManager();
        
        [System.Web.Http.Route("api/teacher/students-by-session")]
        [System.Web.Http.HttpGet]
        [MyAuth(Enum.Role.TEACHER)]
        public IHttpActionResult GetStudentsBySession(int sessionId)
        {
            var session = db.Sessions.Find(sessionId);
            if (session == null)
            {
                return BadRequest("Session does not exist!");
            }
            var course = db.Courses.Find(session.CourseId);
            if (course == null)
            {
                return BadRequest();
            }
            var students = (from courseStudent in db.CourseStudents
                where courseStudent.CourseId == course.Id
                select new StudentDto()
                {
                    rollNumber = courseStudent.Student.RollNumber,
                    firstName = courseStudent.Student.FirstName,
                    lastName = courseStudent.Student.LastName,
                    gender = courseStudent.Student.Gender,
                    dateOfBirth = courseStudent.Student.DateOfBirth,
                    identityNumber = courseStudent.Student.IdentityNumber,
                    identityDateOfIssue = courseStudent.Student.IdentityDateOfIssue,
                    address = courseStudent.Student.Address,
                    phoneNumber = courseStudent.Student.PhoneNumber,
                    email = courseStudent.Student.Email,
                    parentFullName = courseStudent.Student.ParentFullName,
                    parentAddress = courseStudent.Student.ParentAddress,
                    parentPhoneNumber = courseStudent.Student.ParentPhoneNumber,
                    parentEmail = courseStudent.Student.ParentEmail,
                    parentOccupation = courseStudent.Student.ParentOccupation,
                    groupName = courseStudent.Student.GroupName,
                    programName = courseStudent.Student.Program.ProgramName
                }).ToList();
            
            return Ok(students);
        }

        [System.Web.Http.Route("api/sessions/{id}")]
        [System.Web.Http.HttpGet]
        [MyAuth(new Enum.Role[] {Enum.Role.TEACHER, Enum.Role.STAFF})]
        public IHttpActionResult GetSessionDetail(int id)   
        {
            var session = db.Sessions.Find(id);
            if (session == null)
            {
                return BadRequest("Session does not exist!");
            }
            var result = new
            {
                id = session.Id,
                groupName = session.Course.GroupName,
                subjectCode = session.Course.SubjectCode,
                subjectName = session.Course.SubjectName,
                teacherName = session.Teacher.FirstName 
                              +" "+ session.Teacher.LastName,
                date = session.Date,
                roomNumber = session.RoomNumber,
                cancelReason = session.CancelReason,
                startTime = session.StartTime,
                endTime = session.EndTime,
                teacherId = session.TeacherId,
                slotId = session.SlotId,
                courseId = session.CourseId,
                status = session.Status
            };

            return Ok(result);
        }
    }
}
