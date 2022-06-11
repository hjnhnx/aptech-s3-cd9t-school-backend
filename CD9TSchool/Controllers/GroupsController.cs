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
using AutoMapper;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Mapper;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;

namespace CD9TSchool.Controllers
{
    public class GroupsController : ApiController
    {
        private SchoolManager db = new SchoolManager();
        private readonly IMapper mapper = MapperConfig._mapper;

        // GET: api/Groups
        [MyAuth(Utils.Enum.Role.STAFF)]
        public IHttpActionResult GetGroups(int currentPage = 1, int perPage = 10)
        {
            var group = (from groups in db.Groups where groups.DeletedAt == null
                         select groups);
            var items = group.OrderBy(p => p.CreatedAt).Skip(currentPage * perPage - perPage).Take(perPage).ToList();
            var data = items.AsEnumerable().Select(groups => new GroupDto()
            {
                groupName = groups.GroupName,
                programId = groups.ProgramId,
                termId = groups.TermId,
                slotId = groups.SlotId,
                roomNumber = groups.RoomNumber,
                daysOfWeek = groups.DaysOfWeek,
                startDate = groups.StartDate,
                teacherId = groups.TeacherId,
                createdAt = groups.CreatedAt,
                updatedAt = groups.UpdatedAt,
                deletedAt = groups.DeletedAt,
                createdBy = groups.CreatedBy,
                updatedBy = groups.UpdatedBy,
                deletedBy = groups.DeletedBy,
                slotName = groups.Slot.Name,
                teacherName = $"{groups.Teacher.FirstName} {groups.Teacher.LastName}",
                programName = groups.Program.ProgramName,
                programCode = groups.Program.ProgramCode,
                termNumber = groups.Term.TermNumber
            });
            var count = group.Count();
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
        [Route("api/groups/all")]
        public IHttpActionResult GetAllGroups()
        {
            var result = from groups in db.Groups
                         where groups.DeletedAt == null
                         select mapper.Map<GroupDto>(groups);

            return Ok(result.ToList());
        }

        // GET: api/Groups/5
        [ResponseType(typeof(Group))]
        [MyAuth(Utils.Enum.Role.STAFF)]
        public IHttpActionResult GetGroup(string id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<GroupDto>(group));
        }

        // PUT: api/Groups/5
        [MyAuth(Utils.Enum.Role.STAFF)]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroup(string id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.GroupName)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(mapper.Map<GroupDto>(group));
        }

        // POST: api/Groups
        [ResponseType(typeof(Group))]
        [MyAuth(Utils.Enum.Role.STAFF)]
        public IHttpActionResult PostGroup(GroupDto group)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slot = db.Slots.Find(group.slotId);
            var year = DateTime.Now.Year.ToString().Substring(2);
            var month = DateTime.Now.ToString("MM");
            var groupName = $"T{year}{month}{slot.GroupNameSuffix}";

            var existItemCount = db.Groups.Where(p => p.GroupName.Contains(groupName)).Count();

            if(existItemCount != 0) {
                var duplicateGroupName = $"{groupName}{existItemCount}";
                group.groupName = duplicateGroupName;
            } else
            {
                group.groupName = groupName;
            }            
            group.createdAt = DateTime.Now;
            db.Groups.Add(new Group()
            {
                GroupName = group.groupName,
                TeacherId = group.teacherId,
                SlotId = group.slotId,
                TermId = group.termId,
                ProgramId = group.programId,
                DaysOfWeek = group.daysOfWeek,
                RoomNumber = group.roomNumber,
                StartDate = group.startDate,
                CreatedAt = group.createdAt,
                CreatedBy = authUser.Email
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GroupExists(group.groupName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = group.groupName }, mapper.Map<GroupDto>(group));
        }

        // DELETE: api/Groups/5
        [MyAuth(Utils.Enum.Role.STAFF)]
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(string id)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            group.DeletedAt = DateTime.Now;
            group.DeletedBy = authUser.Email;
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
        [MyAuth(Utils.Enum.Role.TEACHER)]
        [ResponseType(typeof(List<Group>))]
        [System.Web.Http.Route("api/teacher/groups")]
        public IHttpActionResult GetGroupByTeacher()
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var teacherId  = (from teacher in db.Teachers where teacher.UserId == authUser.Id select teacher.Id).FirstOrDefault();
            if (teacherId == null) return BadRequest();
            var response = db.Groups.Where(groups => groups.DeletedAt == null && groups.TeacherId == teacherId).Select(groups => new GroupDto()
            {
                groupName = groups.GroupName,
                programId = groups.ProgramId,
                termId = groups.TermId,
                slotId = groups.SlotId,
                roomNumber = groups.RoomNumber,
                daysOfWeek = groups.DaysOfWeek,
                startDate = groups.StartDate,
                teacherId = groups.TeacherId,
                createdAt = groups.CreatedAt,
                updatedAt = groups.UpdatedAt,
                deletedAt = groups.DeletedAt,
                createdBy = groups.CreatedBy,
                updatedBy = groups.UpdatedBy,
                deletedBy = groups.DeletedBy,
                slotName = groups.Slot.Name,
                programName = groups.Program.ProgramName,
                programCode = groups.Program.ProgramCode,
                termNumber = groups.Term.TermNumber
            });
            
            return Ok(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(string id)
        {
            return db.Groups.Count(e => e.GroupName == id) > 0;
        }

        [HttpPost]
        [Route("api/groups/import/{id}")]
        [MyAuth(Utils.Enum.Role.STAFF)]
        public IHttpActionResult Import(string id, [FromBody]ImportStudentDto students)
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var emails = from email in db.Users select email.Email;
            foreach(var checkEmails in students.students)
            {
                var emailExist = emails.Where(d => d.Equals(checkEmails.email)).FirstOrDefault();
                if(emailExist != null)
                {
                    return BadRequest($"Email {emailExist} has already been used.");
                }
            };

            // Begin transaction
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try {
                    var group = db.Groups.Find(id);
                    var users = new List<User>();
                    foreach (var item in students.students)
                    {

                        var user = new User()
                        {
                            Email = item.email,
                            Role = Utils.Enum.Role.STUDENT,
                            CreatedAt = DateTime.Now,
                            CreatedBy = authUser.Email,
                        };

                        users.Add(user);
                    }

                    var userModels = db.Users.AddRange(users);
                    db.SaveChanges();


                    var year = DateTime.Now.Year.ToString().Substring(2);
                    var month = DateTime.Now.ToString("MM");
                    var code = $"TH{year}{month}";

                    var lastItem = (from item in db.Students
                                    where item.RollNumber.StartsWith(code)
                                    orderby item.RollNumber
                                    select item.RollNumber
                                    ).ToList().LastOrDefault();
                    var lastNumber = 0;
                    if (lastItem != null)
                    {
                        lastNumber = int.Parse(lastItem.Substring(6));
                    }

                    var items = new List<Student>();

                    foreach (var obj in students.students)
                    {
                        var userId = userModels.Where(x => x.Email.Equals(obj.email)).Select(x => x.Id).FirstOrDefault();
                        if (userId > 0)
                        {
                            lastNumber = lastNumber + 1;
                            var student = new Student()
                            {
                                FirstName = obj.firstName,
                                LastName = obj.lastName,
                                RollNumber = $"{code}{lastNumber.ToString().PadLeft(3, '0')}",
                                Address = obj.address,
                                Gender = obj.gender,
                                ProgramId = group.ProgramId,
                                PhoneNumber = obj.phoneNumber,
                                DateOfBirth = obj.dateOfBirth,
                                Email = obj.email,
                                IdentityNumber = obj.identityNumber,
                                IdentityDateOfIssue = obj.identityDateOfIssue,
                                IdentityPlaceOfIssue = obj.identityPlaceOfIssue,
                                ParentEmail = obj.parentEmail,
                                ParentAddress = obj.parentAddress,
                                ParentFullName = obj.parentFullName,
                                ParentOccupation = obj.parentOccupation,
                                ParentPhoneNumber = obj.parentPhoneNumber,
                                GroupName = id,
                                CreatedAt = DateTime.Now,
                                CreatedBy = authUser.Email,
                                UserId = userId,
                            };
                            items.Add(student);
                        }
                    }

                    var listStudent = db.Students.AddRange(items);
                    db.SaveChanges();

                    dbContextTransaction.Commit();

                    return Ok(new
                    {
                        studentsAdded = listStudent.Count()
                    });
                } catch(Exception e)
                {
                    dbContextTransaction.Rollback();

                    return BadRequest(e.Message);
                }
            }
        }

        [HttpGet]
        [Route("api/teacher/groups-from-courses")]
        [MyAuth(Utils.Enum.Role.TEACHER)]
        public IHttpActionResult GetGroupFromCourse()
        {
            var authUser = HttpContext.Current.User as MyPrincipal;
            var authTeacher = (from teacher in db.Teachers where teacher.UserId == authUser.Id select teacher).First();
            var groups = (from course in db.Courses
                where course.TeacherId == authTeacher.Id
                select new GroupDto()
                {
                    groupName = course.GroupName,
                    teacherId = course.TeacherId,
                    slotId = course.SlotId,
                    programId = course.Group.ProgramId,
                    termId = course.TermId,
                    daysOfWeek = course.Group.DaysOfWeek,
                    roomNumber = course.RoomNumber,
                    startDate = course.Group.StartDate,
                    teacherName = course.Teacher.FirstName + " " + course.Teacher.LastName,
                    slotName = course.Slot.Name,
                    programName = course.Group.Program.ProgramName,
                    programCode = course.Group.Program.ProgramCode,
                    termNumber = course.Term.TermNumber,
                    hasGeneratedTimeTable = course.Group.HasGeneratedTimetable,
                    createdAt = course.Group.CreatedAt,
                    updatedAt = course.Group.UpdatedAt,
                    deletedAt = course.Group.DeletedAt,
                    createdBy = course.Group.CreatedBy,
                    updatedBy = course.Group.UpdatedBy,
                    deletedBy = course.Group.DeletedBy
                }).ToList().GroupBy(group=> group.groupName).Select(group => group.First());
            
            return Ok(groups);
        }
    }
}