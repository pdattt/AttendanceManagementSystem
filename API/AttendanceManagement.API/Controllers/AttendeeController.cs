using AttendanceManagement.Common.Dtos;
using AttendanceManagement.Common.Dtos.AttendeeDTOs;
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

            if (attendees.Count > 0)
                return Ok(attendees);

            return NotFound();
        }

        [Route("get-attendee-by-id")]
        [HttpGet]
        public ActionResult GetAttendeeById(int id)
        {
            AttendeeReadDTO attendee = _service.GetById(id);

            if (attendee != null)
                return Ok(attendee);

            return NotFound();
        }

        [Route("add-new-attendee")]
        [HttpPost]
        public ActionResult AddNewAttendee([FromBody] AttendeeCreateDTO attendeeDTO)
        {
            if (_service.Add(attendeeDTO))
                return Ok();

            return BadRequest("Email has exited!");
        }

        [Route("delete-attendee-by-id")]
        [HttpDelete]
        public ActionResult DeleteAttendeeById(int id)
        {
            bool checkDelete = _service.Delete(id);

            if (checkDelete)
                return Ok();

            return NotFound();
        }

        [Route("update-attendee-by-id")]
        [HttpPut]
        public ActionResult UpdateAttendeeById([FromBody] AttendeeUpdateDTO attendeeUpdate, int id)
        {
            bool checkUpdate = _service.Update(attendeeUpdate, id);

            if (checkUpdate)
                return Ok();

            return BadRequest();
        }
    }
}