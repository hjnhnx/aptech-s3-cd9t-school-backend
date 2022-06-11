using System.Linq;
using System.Web.Http;
using CD9TSchool.App_Start;
using CD9TSchool.Data;
using CD9TSchool.Models.Dto;
using CD9TSchool.Utils;

namespace CD9TSchool.Controllers
{
    public class RoomsController : ApiController
    {
        private SchoolManager db = new SchoolManager();

        // GET: api/rooms
        [Route("api/rooms")]
        [HttpGet]
        [MyAuth(Enum.Role.STAFF)]
        public IHttpActionResult GetRooms()
        {
            var result = from room in db.Rooms
                         where room.DeletedAt == null
                         select new RoomDto()
            {
                roomNumber = room.RoomNumber,
                capacity = room.Capacity,
                roomType = room.RoomType,
                description = room.Description,
                isDisabled = room.IsDisabled,
                createdAt = room.CreatedAt,
                updatedAt = room.UpdatedAt,
                deletedAt = room.DeletedAt,
                createdBy = room.CreatedBy,
                updatedBy = room.UpdatedBy,
                deletedBy = room.DeletedBy,
            };
            
            return Ok(result.ToList());
        }
        
        // GET: api/rooms/open
        [Route("api/rooms/open")]
        [HttpGet]
        [MyAuth(Enum.Role.STAFF)]
        public IHttpActionResult GetRoomsOpen()
        {
            var result = from room in db.Rooms 
                where room.IsDisabled == false
                select new RoomDto()
            {
                roomNumber = room.RoomNumber,
                capacity = room.Capacity,
                roomType = room.RoomType,
                description = room.Description,
                isDisabled = room.IsDisabled,
                createdAt = room.CreatedAt,
                updatedAt = room.UpdatedAt,
                deletedAt = room.DeletedAt,
                createdBy = room.CreatedBy,
                updatedBy = room.UpdatedBy,
                deletedBy = room.DeletedBy,
            };
            
            return Ok(result.ToList());
        }
    }
}