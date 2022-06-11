using System.Linq;
using System.Web.Http;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models.Dto;

namespace CD9TSchool.Controllers
{
    public class SlotsController : ApiController
    {
        private SchoolManager db = new SchoolManager();

        // GET: api/slots
        [Route("api/slots")]
        [HttpGet]
        [MyAuth]
        public IHttpActionResult GetSlots()
        {
            var result = from slot in db.Slots
                         where slot.DeletedAt == null
                         select new SlotDto()
                {
                    id = slot.Id,
                    name = slot.Name,
                    groupNameSuffix = slot.GroupNameSuffix,
                    startTime = slot.StartTime,
                    endTime = slot.EndTime,
                    createdAt = slot.CreatedAt,
                    updatedAt = slot.UpdatedAt,
                    deletedAt = slot.DeletedAt,
                    createdBy = slot.CreatedBy,
                    updatedBy = slot.UpdatedBy,
                    deletedBy = slot.DeletedBy,
                };

            return Ok(result.ToList());
        }
    }
}