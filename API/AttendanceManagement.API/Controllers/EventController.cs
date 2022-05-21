using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [Route("/get-all-events")]
        [HttpGet]
        public ActionResult GetAllEvents()
        {
            return Ok();
        }
    }
}