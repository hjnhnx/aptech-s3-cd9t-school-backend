using System.Linq;
using System.Web.Http;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models.Dto;

namespace CD9TSchool.Controllers
{
    public class ProgramsController : ApiController
    {
        private SchoolManager db = new SchoolManager();

        // GET: api/programs
        [Route("api/programs")]
        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetPrograms()
        {
            var result = from program in db.Programs
                         where program.DeletedAt == null
                         select new ProgramDto()
                {
                    id = program.Id,
                    programCode = program.ProgramCode,
                    programName = program.ProgramName,
                    createdAt = program.CreatedAt,
                    updatedAt = program.UpdatedAt,
                    deletedAt = program.DeletedAt,
                    createdBy = program.CreatedBy,
                    updatedBy = program.UpdatedBy,
                    deletedBy = program.DeletedBy,
                    terms = from term in db.Terms where term.ProgramId == program.Id select new TermDto()
                    {
                            id = term.Id,
                            programId = program.Id,
                            termNumber = term.TermNumber,
                            createdAt = term.CreatedAt,
                            updatedAt = term.UpdatedAt,
                            deletedAt = term.DeletedAt,
                            createdBy = term.CreatedBy,
                            updatedBy = term.UpdatedBy,
                            deletedBy = term.DeletedBy
                    }
                };

            return Ok(result.ToList());
        }
    }
}