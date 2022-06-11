using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models.Dto;
using Enum = CD9TSchool.Utils.Enum;

namespace CD9TSchool.Controllers
{
    public class TeacherController : ApiController
    {
        private SchoolManager db = new SchoolManager();
        
        [Route("api/teachers")]
        [HttpGet]
        [MyAuth(Enum.Role.STAFF)]
        //Get /api/teachers
        public IHttpActionResult GetTeachers()
        {
            var result = (from teacher in db.Teachers select new TeacherDto()
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                lastName = teacher.LastName
            });
            var data = result.ToList();

            return Ok(data);
        }
    }
}
