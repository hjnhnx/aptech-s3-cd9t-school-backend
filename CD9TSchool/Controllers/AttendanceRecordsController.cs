using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;
using Enum = CD9TSchool.Utils.Enum;

namespace CD9TSchool.Controllers
{
    public class AttendanceRecordsController : ApiController
    {
        private SchoolManager db;

        public AttendanceRecordsController()
        {
            db = new SchoolManager();
        }

        [System.Web.Http.Route("api/teacher/attendance")]
        [MyAuth(Enum.Role.TEACHER)]
        public async Task<HttpStatusCodeResult> save(List<AttendanceRecordDto> records, int sessionId)
        {
            var dataUser = HttpContext.Current.User as MyPrincipal;
            var teacher = db.Teachers.FirstOrDefault(m => m.UserId == dataUser.Id);
            var session = db.Sessions.FirstOrDefault(m => m.Id == sessionId);

            if (teacher.Id != session.TeacherId) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (session.Status != Enum.SessionStatus.PENDING)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            session.Status = Enum.SessionStatus.TAKEN;
            db.Entry(session).State = EntityState.Modified;

            if (DateTime.Now.TimeOfDay.TotalMinutes < (session.EndTime + 30) &&
                DateTime.Now.TimeOfDay.TotalMinutes > session.StartTime && session.Date == DateTime.Now.Date)
            {
                var attendanceRecords = new List<AttendanceRecord>();
                foreach (var item in records)
                    attendanceRecords.Add(new AttendanceRecord
                    {
                        SessionId = sessionId,
                        CourseId = session.CourseId,
                        StudentRollNumber = item.studentRollNumber,
                        IsPresent = item.isPresent,
                        Comment = item.comment,
                        CreatedBy = dataUser.Email,
                        CreatedAt = DateTime.Now
                    });
                db.AttendanceRecords.AddRange(attendanceRecords);
                db.SaveChanges();
                var count = db.Sessions.Count(m => m.Status == Enum.SessionStatus.TAKEN);
                var course = db.Courses.FirstOrDefault(m => m.Id == session.CourseId);

                if (count == course.SessionTotal)
                {
                    course.Status = Enum.CourseStatus.FINISHED;
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return new HttpStatusCodeResult(HttpStatusCode.Created);   
            }
            session.Status = Enum.SessionStatus.MISSING;
            db.Entry(session).State = EntityState.Modified;
            db.SaveChanges();
            
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [System.Web.Http.Route("api/attendance")]
        [MyAuth(Enum.Role.STAFF)]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAttendanceReport(int courseId)
        {
            var sessionsQueryResult = (from session in db.Sessions
                where session.CourseId == courseId
                select new SessionDto()
                {
                    id = session.Id,
                    groupName = session.Course.GroupName,
                    subjectCode = session.Course.SubjectCode,
                    subjectName = session.Course.SubjectName,
                    teacherName = session.Teacher.FirstName + " " + session.Teacher.LastName,
                    date = session.Date,
                    roomNumber = session.RoomNumber,
                    cancelReason = session.CancelReason,
                    startTime = session.StartTime,
                    endTime = session.EndTime,
                    teacherId = session.TeacherId,
                    slotId = session.SlotId,
                    courseId = session.CourseId,
                    status = session.Status
                }).ToList();
            
            var attendanceQueryResult = (from attendance in db.AttendanceRecords
                    where attendance.CourseId == courseId
                    group new AttendanceReportDto()
                    {
                        id = attendance.Id,
                        sessionId = attendance.SessionId,
                        courseId = attendance.CourseId,
                        studentRollNumber = attendance.StudentRollNumber,
                        isPresent = attendance.IsPresent,
                        comment = attendance.Comment,
                        studentFullName = attendance.Student.FirstName + " " + attendance.Student.LastName
                    } by attendance.StudentRollNumber
                ).ToList();

            var attendanceReportResult = new Dictionary<string, List<Object>>();
            
            var attendanceRecords = new List<Object>();
            var sessions = new List<Object>();

            foreach (var session in sessionsQueryResult)
            {
                sessions.Add(new
                {
                    id = session.id,
                    groupName = session.groupName,
                    subjectCode = session.subjectCode,
                    subjectName = session.subjectName,
                    teacherName = session.teacherName,
                    date = session.date,
                    roomNumber = session.roomNumber,
                    cancelReason = session.cancelReason,
                    startTime = session.startTime,
                    endTime = session.endTime,
                    teacherId = session.teacherId,
                    slotId = session.slotId,
                    courseId = session.courseId,
                    status = session.status
                });
            }

            foreach (var attendances in attendanceQueryResult)
            {
                var FirstAttendance = attendances.ToList()[0];
                attendanceRecords.Add(new
                {
                    studentRollnumber = FirstAttendance.studentRollNumber,
                    studentFullName = FirstAttendance.studentFullName,
                    attendances = attendances
                });
            }
            
            attendanceReportResult.Add("attendanceRecords", attendanceRecords);
            attendanceReportResult.Add("sessions", sessions);

            return Ok(attendanceReportResult);
        }

        [System.Web.Http.Route("api/teacher/attendance-by-session")]
        [MyAuth(Enum.Role.TEACHER)]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAttendanceRecordBySessionWithTeacher(int sessionId)
        {
            var dataUser = HttpContext.Current.User as MyPrincipal;
            var teacher = db.Teachers.FirstOrDefault(m => m.UserId == dataUser.Id);
            if (teacher == null || teacher.DeletedAt != null) return BadRequest();
            var session = db.Sessions.FirstOrDefault(m => m.Id == sessionId && m.TeacherId == teacher.Id);
            if (session == null || session.Status == Enum.SessionStatus.PENDING) return BadRequest();

            var response = (from attendance in db.AttendanceRecords
                    where attendance.SessionId == sessionId
                    select new AttendanceReportDto()
                    {
                        id = attendance.Id,
                        sessionId = attendance.SessionId,
                        courseId = attendance.CourseId,
                        studentRollNumber = attendance.StudentRollNumber,
                        isPresent = attendance.IsPresent,
                        comment = attendance.Comment,
                        studentFullName = attendance.Student.FirstName + " " + attendance.Student.LastName,
                        date = attendance.CreatedAt
                    }
                ).ToList();
            return Ok(response);

        }
        [System.Web.Http.Route("api/student/attendance")]
        [MyAuth(Enum.Role.STUDENT)]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAttendanceRecordByCourseWithStudent(int courseId)
        {
            var dataUser = HttpContext.Current.User as MyPrincipal;
            var student = db.Students.FirstOrDefault(m => m.UserId == dataUser.Id);
            if (student == null || student.DeletedAt != null) return BadRequest();
            var response = (from attendance in db.AttendanceRecords
                            where attendance.CourseId == courseId && attendance.StudentRollNumber == student.RollNumber
                            select new AttendanceReportDto()
                            {
                                id = attendance.Id,
                                sessionId = attendance.SessionId,
                                startTime = attendance.Session.StartTime,
                                endTime = attendance.Session.EndTime,
                                date = attendance.CreatedAt,
                                courseId = attendance.CourseId,
                                subjectCode = attendance.Course.SubjectCode,
                                roomNumber = attendance.Session.RoomNumber,
                                isPresent = attendance.IsPresent,
                                comment = attendance.Comment,
                            }
                ).ToList();
            return Ok(response);
        }
        [System.Web.Http.Route("api/teacher/attendance")]
        [MyAuth(Enum.Role.TEACHER)]
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult EditAttendanceRecord([FromBody] AttendanceRecordDto record)
        {
            var dataUser = HttpContext.Current.User as MyPrincipal;

            var teacher = db.Teachers.FirstOrDefault(m => m.UserId == dataUser.Id);
            var session = db.Sessions.FirstOrDefault(m => m.Id == record.sessionId);

            if (teacher.Id != session.TeacherId) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            if (session == null || session.Status == Enum.SessionStatus.PENDING) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var attendanceRecord = db.AttendanceRecords.Where(x => x.SessionId == record.sessionId && x.StudentRollNumber.Equals(record.studentRollNumber)).FirstOrDefault();

            attendanceRecord.IsPresent = record.isPresent;
            attendanceRecord.Comment = record.comment;
            attendanceRecord.UpdatedAt = DateTime.Now;
            attendanceRecord.UpdatedBy = dataUser.Email;

            var newAttendanceReport = db.Entry(attendanceRecord).State = EntityState.Modified;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK, "Updated success");
        }
    }
}