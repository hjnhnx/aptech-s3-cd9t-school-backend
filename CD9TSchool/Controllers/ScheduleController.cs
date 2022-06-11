using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using AutoMapper;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Mapper;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;
using Enum = CD9TSchool.Utils.Enum;

namespace CD9TSchool.Controllers
{
    public class ScheduleController : ApiController
    {
        private SchoolManager db = new SchoolManager();
        private readonly IMapper mapper = MapperConfig._mapper;
        
        // POST: api/timetable/generate
        [System.Web.Http.Route("api/timetable/generate")]
        [System.Web.Http.HttpPost]
        [MyAuth(Enum.Role.STAFF)]
        public HttpStatusCodeResult GenerateSchedule(string groupName, DateTime startDate)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            // group
            GroupDto group = mapper.Map<GroupDto>(db.Groups.Find(groupName));
            var termId = group.termId;

            // studentRollNumbers[]
            var studentRollNumbers = (from student in db.Students
                where student.GroupName == groupName
                select student.RollNumber).ToList();

            // subjectCodes[]
            var subjects = (from termSubject in db.TermSubjects
                where termSubject.TermId == termId
                select new SubjectDto()
                {
                    subjectCode = termSubject.SubjectCode,
                    subjectName = termSubject.Subject.SubjectName,
                    sessionTotal = termSubject.Subject.SessionTotal
                }).ToList();

            // holidays[]
            var holidays = (from holiday in db.Holidays
                where holiday.StartDate >= startDate
                select holiday).ToList();

            // list of holidays
            List<DateTime> listOfHolidays = new List<DateTime>();
            foreach (var day in holidays)
            {
                for (DateTime d = day.StartDate; d <= day.EndDate ; d = d.AddDays(1))
                {
                    listOfHolidays.Add(d.Date);
                }
            }
            
            // Begin transaction
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Remove comments during dev
                    // var attendanceRecords = (from attendance in db.AttendanceRecords select attendance).ToList();
                    // db.AttendanceRecords.RemoveRange(attendanceRecords);
                    // var sessionssss = (from session in db.Sessions select session).ToList();
                    // db.Sessions.RemoveRange(sessionssss);
                    // var userCoursess = (from uCourse in db.CourseStudents select uCourse).ToList();
                    // db.CourseStudents.RemoveRange(userCoursess);
                    // var coursess = (from course in db.Courses select course).ToList();
                    // db.Courses.RemoveRange(coursess);
                    // db.SaveChanges();

                    // create new courses
                    List<Course> courses = new List<Course>();
                    foreach (var subject in subjects)
                    {
                        courses.Add(new Course()
                        {
                            GroupName = group.groupName,
                            SubjectCode = subject.subjectCode,
                            SubjectName = subject.subjectName,
                            SessionTotal = subject.sessionTotal,
                            TeacherId = group.teacherId,
                            SlotId = group.slotId,
                            TermId = group.termId,
                            RoomNumber = group.roomNumber,
                            CreatedAt = DateTime.Now,
                            CreatedBy = authUser.Email
                        });
                    }

                    db.Courses.AddRange(courses.ToList());
                    db.SaveChanges();
                    // newly created courses
                    var newCourses = (from course in db.Courses
                        where course.TermId == termId && course.GroupName == groupName
                        select course).ToList();

                    // create new CourseStudents
                    List<CourseStudent> courseStudents = new List<CourseStudent>();
                    foreach (var newCourse in newCourses)
                    {
                        foreach (var studentRollNumber in studentRollNumbers)
                        {
                            courseStudents.Add(new CourseStudent()
                            {
                                CourseId = newCourse.Id,
                                StudentRollNumber = studentRollNumber,
                                Comment = "",
                                CreatedAt = DateTime.Now,
                                CreatedBy = authUser.Email
                            });
                        }
                    }
                
                    db.CourseStudents.AddRange(courseStudents.ToList());
                    db.SaveChanges();

                    // slot
                    var slot = db.Slots.Find(group.slotId);

                    // days of Week for group
                    int[] daysOfWeek = Array.ConvertAll(group.daysOfWeek.Split(','),s=>int.Parse(s));
                    
                    var date = startDate;
                    List<DateTime> checkDateList = new List<DateTime>();
                    List<Session> sessions = new List<Session>();
                    foreach (var course in newCourses)
                    {
                        // vòng lặp qua sessionTotal của course, i chỉ ++ khi date thuộc daysOfWeek, mỗi lượt date += 1day
                        for (int i = 0; i < course.SessionTotal;)
                        {
                            if (Array.Exists(daysOfWeek, e => e == (int) date.DayOfWeek) && checkHoliday(listOfHolidays,date) == false)
                            {
                                checkDateList.Add(date.Date);
                                sessions.Add(new Session()
                                {
                                    CourseId = course.Id,
                                    Date = date.Date,
                                    SlotId = slot.Id,
                                    StartTime = slot.StartTime,
                                    EndTime = slot.EndTime,
                                    TeacherId = group.teacherId,
                                    RoomNumber = group.roomNumber,
                                    Status = Enum.SessionStatus.PENDING,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = authUser.Email,
                                    CancelReason = ""
                                });
                                i++;
                            }
                            // next day
                            date = date.AddDays(1);
                        }
                    }
                    
                    var teacherId = group.teacherId;
                    var roomNumber = group.roomNumber;
                    // Check the teacher, or room, is unavailable during selected time slots.
                    var sessionExits = (from session in db.Sessions
                        where session.SlotId == slot.Id &&
                              (session.TeacherId == teacherId || session.RoomNumber == roomNumber) &&
                              checkDateList.Contains(session.Date)
                        select session.Id).ToList();

                    if (sessionExits.Any())
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"The teacher, or room, is unavailable during selected time slots.");
                    }

                    db.Sessions.AddRange(sessions.ToList());
                    db.SaveChanges();
                
                    dbContextTransaction.Commit();
                    
                    return new HttpStatusCodeResult(HttpStatusCode.Created,"Schedule generated successfully!");
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest,e.Message);
                }
            }
        }

        // GET: api/teacher/timetable
        [System.Web.Http.Route("api/teacher/timetable")]
        [System.Web.Http.HttpGet]
        [MyAuth(Enum.Role.TEACHER)]
        public IHttpActionResult GetPersonalTimetable(DateTime startDate, DateTime endDate)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;

            var authTeacher = (from teacher in db.Teachers where teacher.UserId == authUser.Id select teacher).First();

            var sessions = (from session in db.Sessions
                where session.TeacherId == authTeacher.Id &&
                      session.Date >= startDate &&
                      session.Date <= endDate
                group new SessionDto()
                {
                    id = session.Id,
                    groupName = session.Course.Group.GroupName,
                    subjectCode = session.Course.SubjectCode,
                    subjectName = session.Course.SubjectName,
                    teacherName = session.Course.Teacher.FirstName+" "+session.Course.Teacher.LastName,
                    date = session.Date,
                    roomNumber = session.RoomNumber,
                    cancelReason = session.CancelReason,
                    startTime = session.StartTime,
                    endTime = session.EndTime,
                    status = session.Status,
                    teacherId = session.TeacherId,
                    slotId = session.SlotId,
                    courseId = session.CourseId
                } by session.Date).ToList();
            
            var schedule = new Dictionary<string, IGrouping<DateTime, SessionDto>>();
            foreach (var session in sessions)
            {
                schedule.Add(session.Key.Date.ToString("yyyy-MM-dd"),session);
            }
            
            // holidays[]
            var holidays = (from holiday in db.Holidays
                where holiday.StartDate >= startDate && holiday.EndDate <= endDate
                select holiday).ToList();

            // list of holidays
            var listOfHolidays = new Dictionary<string, string>();
            
            foreach (var day in holidays)
            {
                for (DateTime d = day.StartDate; d <= day.EndDate ; d = d.AddDays(1))
                {
                    listOfHolidays.Add(d.Date.ToString("yyyy-MM-dd"),day.Name);
                }
            }

            return Ok(new
            {
                schedule = schedule,
                holidays = listOfHolidays
            });
        }
        
        // GET: api/staff/timetable
        [System.Web.Http.Route("api/staff/timetable")]
        [System.Web.Http.HttpGet]
        [MyAuth(Enum.Role.STAFF)]
        public IHttpActionResult GetAllTimetable(DateTime startDate, DateTime endDate)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;

            var sessions = (from session in db.Sessions
                where session.Date >= startDate &&
                      session.Date <= endDate
                group new SessionDto()
                {
                    id = session.Id,
                    groupName = session.Course.Group.GroupName,
                    subjectCode = session.Course.SubjectCode,
                    subjectName = session.Course.SubjectName,
                    teacherName = session.Course.Teacher.FirstName+" "+session.Course.Teacher.LastName,
                    date = session.Date,
                    roomNumber = session.RoomNumber,
                    cancelReason = session.CancelReason,
                    startTime = session.StartTime,
                    endTime = session.EndTime,
                    status = session.Status,
                    teacherId = session.TeacherId,
                    slotId = session.SlotId,
                    courseId = session.CourseId
                } by session.Date).ToList();
            
            var schedule = new Dictionary<string, IGrouping<DateTime, SessionDto>>();
            foreach (var session in sessions)
            {
                schedule.Add(session.Key.Date.ToString("yyyy-MM-dd"),session);
            }

            // holidays[]
            var holidays = (from holiday in db.Holidays
                where holiday.StartDate >= startDate && holiday.EndDate <= endDate
                select holiday).ToList();

            // list of holidays
            var listOfHolidays = new Dictionary<string, string>();
            
            foreach (var day in holidays)
            {
                for (DateTime d = day.StartDate; d <= day.EndDate ; d = d.AddDays(1))
                {
                    listOfHolidays.Add(d.Date.ToString("yyyy-MM-dd"),day.Name);
                }
            }

            return Ok(new
            {
                schedule = schedule,
                holidays = listOfHolidays
            });
        }

        // GET: api/teacher/sessions/today
        [System.Web.Http.Route("api/teacher/sessions/today")]
        [System.Web.Http.HttpGet]
        [MyAuth(Enum.Role.TEACHER)]
        public IHttpActionResult GetSessionToday()
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var today = DateTime.Now;
            var authTeacher = (from teacher in db.Teachers where teacher.UserId == authUser.Id select teacher).First();
            var sessions = (from session in db.Sessions
                where session.Date.Equals(today.Date) && session.TeacherId == authTeacher.Id
                orderby session.StartTime
                select new SessionDto()
                {
                    id = session.Id,
                    groupName = session.Course.Group.GroupName,
                    subjectCode = session.Course.SubjectCode,
                    subjectName = session.Course.SubjectName,
                    teacherName = session.Course.Teacher.FirstName+" "+session.Course.Teacher.LastName,
                    date = session.Date,
                    roomNumber = session.RoomNumber,
                    cancelReason = session.CancelReason,
                    startTime = session.StartTime,
                    endTime = session.EndTime,
                    status = session.Status,
                    teacherId = session.TeacherId,
                    slotId = session.SlotId,
                    courseId = session.CourseId
                }).ToList();
            
            return Ok(sessions);
        }
        
        // GET: api/student/timetable
        [System.Web.Http.Route("api/student/timetable")]
        [System.Web.Http.HttpGet]
        [MyAuth(Enum.Role.STUDENT)]
        public IHttpActionResult GetTimetableStudent(DateTime startDate, DateTime endDate)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var studentRollNumber = (from student in db.Students 
                where student.UserId == authUser.Id select student.RollNumber).First();
            
            var querySessions = (from courseStudent in db.CourseStudents
                where courseStudent.StudentRollNumber == studentRollNumber
                select courseStudent.Course.Sessions.Where(s => s.Date >= startDate &&
                                                                         s.Date <= endDate)
                ).ToList();
            List<SessionDto> result = new List<SessionDto>();

            foreach (var sessions in querySessions)
            {
                foreach (var session in sessions)
                {
                    result.Add(new SessionDto()
                    {
                        id = session.Id,
                        groupName = session.Course.GroupName,
                        subjectCode = session.Course.SubjectCode,
                        subjectName = session.Course.SubjectName,
                        teacherName = session.Teacher.LastName +" "+session.Teacher.FirstName,
                        date = session.Date,
                        roomNumber = session.RoomNumber,
                        cancelReason = session.CancelReason,
                        startTime = session.StartTime,
                        endTime = session.EndTime,
                        teacherId = session.TeacherId,
                        slotId = session.SlotId,
                        courseId = session.CourseId,
                        status = session.Status
                    });
                }
            }

            var schedule = new Dictionary<string,List<SessionDto>>();
            foreach (var sessionDto in result.ToList().GroupBy(e => e.date))
            {
                schedule.Add(sessionDto.Key.Date.ToString("yyyy-MM-dd"), sessionDto.ToList());
            }
            
            var holidays = (from holiday in db.Holidays
                where holiday.StartDate >= startDate && holiday.EndDate <= endDate
                select holiday).ToList();
            
            var listOfHolidays = new Dictionary<string, string>();
            foreach (var day in holidays)
            {
                for (DateTime d = day.StartDate; d <= day.EndDate ; d = d.AddDays(1))
                {
                    listOfHolidays.Add(d.Date.ToString("yyyy-MM-dd"),day.Name);
                }
            }
            
            return Ok(new
            {
                schedule = schedule,
                holidays = listOfHolidays
            });
        }

        // check date is holiday
        private bool checkHoliday(List<DateTime> holidays , DateTime day)
        {
            if (holidays.Contains(day.Date))
            {
                return true;
            }
            
            return false;
        }
    }
}
