using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;

namespace CD9TSchool.Controllers
{
    public class StudentsController : ApiController
    {
        private SchoolManager db = new SchoolManager();


        [MyAuth(Utils.Enum.Role.STAFF)]
        // GET: api/Students
        public IHttpActionResult GetStudents(int currentPage = 1, int perPage = 10)
        {
            var student = (from students in db.Students where students.DeletedAt == null select students);
            var items = student.OrderBy(p => p.RollNumber).Skip(currentPage * perPage - perPage).Take(perPage).ToList();
            var data = items.AsEnumerable().Select(students => new StudentDto() {
                rollNumber = students.RollNumber,
                firstName = students.FirstName,
                lastName = students.LastName,
                gender = students.Gender,
                address = students.Address,
                email = students.Email,
                dateOfBirth = students.DateOfBirth,
                phoneNumber = students.PhoneNumber,
                identityNumber = students.IdentityNumber,
                identityDateOfIssue = students.IdentityDateOfIssue,
                identityPlaceOfIssue = students.IdentityPlaceOfIssue,
                parentAddress = students.ParentAddress,
                parentEmail = students.ParentEmail,
                parentFullName = students.ParentFullName,
                parentOccupation = students.ParentOccupation,
                parentPhoneNumber = students.PhoneNumber,
                programName = students.Program.ProgramName,
                groupName = students.GroupName
            });

            var count = student.Count();
            var result = new
            {
                data,
                perPage,
                currentPage,
                totalPages = (int)Math.Ceiling((decimal)count / perPage),
                count,
            };
            return Ok(result);
        }


        [MyAuth(Utils.Enum.Role.STAFF)]
        [Route("api/students/group/{id}")]
        public IHttpActionResult GetStudentsByGroupName(string id)
        {
            var items = from students in db.Students
                           where students.GroupName.Equals(id)
                           select new StudentDto()
                           {
                               rollNumber = students.RollNumber,
                               firstName = students.FirstName,
                               lastName = students.LastName,
                               gender = students.Gender,
                               address = students.Address,
                               email = students.Email,
                               dateOfBirth = students.DateOfBirth,
                               phoneNumber = students.PhoneNumber,
                               identityNumber = students.IdentityNumber,
                               identityDateOfIssue = students.IdentityDateOfIssue,
                               identityPlaceOfIssue = students.IdentityPlaceOfIssue,
                               parentAddress = students.ParentAddress,
                               parentEmail = students.ParentEmail,
                               parentFullName = students.ParentFullName,
                               parentOccupation = students.ParentOccupation,
                               parentPhoneNumber = students.PhoneNumber,
                               programName = students.Program.ProgramName,
                               groupName = students.GroupName
                           };
            return Ok(items);
        }
        [MyAuth(Utils.Enum.Role.TEACHER)]
        [Route("api/teacher/students-by-group")]
        public IHttpActionResult GetStudentsByGroupNameWithTeacher(string groupName)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var teacherId  = (from teacher in db.Teachers where teacher.UserId == authUser.Id select teacher.Id).FirstOrDefault();
            if (teacherId == null) return BadRequest();
            var confirmGroupName = db.Groups.Where(groups => groups.DeletedAt == null && groups.TeacherId == teacherId && groups.GroupName == groupName)
                .Select(groups => groups.GroupName).FirstOrDefault();
            if (confirmGroupName == null) return BadRequest();
            var response = from students in db.Students
                where students.GroupName.Equals(confirmGroupName)
                select new StudentDto()
                {
                    rollNumber = students.RollNumber,
                    firstName = students.FirstName,
                    lastName = students.LastName,
                    gender = students.Gender,
                    address = students.Address,
                    email = students.Email,
                    dateOfBirth = students.DateOfBirth,
                    phoneNumber = students.PhoneNumber,
                    identityNumber = students.IdentityNumber,
                    identityDateOfIssue = students.IdentityDateOfIssue,
                    identityPlaceOfIssue = students.IdentityPlaceOfIssue,
                    parentAddress = students.ParentAddress,
                    parentEmail = students.ParentEmail,
                    parentFullName = students.ParentFullName,
                    parentOccupation = students.ParentOccupation,
                    parentPhoneNumber = students.PhoneNumber,
                    programName = students.Program.ProgramName,
                    groupName = students.GroupName
                };
            return Ok(response);
        }
        [MyAuth(Utils.Enum.Role.STUDENT)]
        [Route("api/student/profile")]
        public IHttpActionResult GetPersonalInformation()
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var response  = db.Students.Where(m => m.DeletedAt == null && m.UserId == authUser.Id).Select(student =>  new StudentDto()
            {
                rollNumber = student.RollNumber,
                firstName = student.FirstName,
                lastName = student.LastName,
                gender = student.Gender,
                address = student.Address,
                email = student.Email,
                dateOfBirth = student.DateOfBirth,
                phoneNumber = student.PhoneNumber,
                identityNumber = student.IdentityNumber,
                identityDateOfIssue = student.IdentityDateOfIssue,
                identityPlaceOfIssue = student.IdentityPlaceOfIssue,
                parentAddress = student.ParentAddress,
                parentEmail = student.ParentEmail,
                parentFullName = student.ParentFullName,
                parentOccupation = student.ParentOccupation,
                parentPhoneNumber = student.PhoneNumber,
                programName = student.Program.ProgramName,
                groupName = student.GroupName
            }).FirstOrDefault();
            if (response == null) return BadRequest();
            return Ok(response);
        }
    }
}