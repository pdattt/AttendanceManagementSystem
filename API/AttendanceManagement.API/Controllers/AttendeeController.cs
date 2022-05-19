using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        [Route("get-all-attendees")]
        public ActionResult GetAllAttendees()
        {
            return Ok();
        }
    }
}
