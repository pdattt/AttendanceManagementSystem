using AttendanceManagement.Common.Dtos;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly IAttendeeService _service;
        public AttendeeController(IAttendeeService service)
        {
            _service = service;
        }

        [Route("get-all-attendees")]
        [HttpGet]
        public ActionResult GetAllAttendees()
        {
            List<AttendeeReadDTO> attendees = _service.GetAll();

            return Ok(attendees);
        }
    }
}
