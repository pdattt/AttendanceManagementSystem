using AttendanceManagement.Common.Dtos;
using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [EnableCors]
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
            var attendees = _service.GetAll();

            if (attendees.Count < 1)
                return BadRequest();

            return Ok(attendees);
        }

        [Route("get-attendee-by-id")]
        [HttpGet]
        public ActionResult GetAttendeeById(int id)
        {
            var attendee = _service.GetById(id);

            if (attendee == null)
                return NotFound();

            return Ok(attendee);
        }

        [Route("add-new-attendee")]
        [HttpPost]
        public ActionResult AddNewAttendee([FromBody] AttendeeCreateDTO attendeeDTO)
        {
            bool checkAdd = _service.Add(attendeeDTO);

            if (!checkAdd)
                return BadRequest();

            return Ok(); ;
        }

        [Route("delete-attendee-by-id")]
        [HttpDelete]
        public ActionResult DeleteAttendeeById(int id)
        {
            bool checkDelete = _service.Delete(id);

            if (!checkDelete)
                return NotFound();

            return Ok();
        }

        [Route("update-attendee-by-id/")]
        [HttpPut]
        public ActionResult UpdateAttendeeById([FromBody] AttendeeUpdateDTO attendeeUpdate, int id)
        {
            bool checkUpdate = _service.Update(attendeeUpdate, id);

            if (!checkUpdate)
                return BadRequest();

            return Ok();
        }
    }
}