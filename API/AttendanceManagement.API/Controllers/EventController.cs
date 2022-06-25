using AttendanceManagement.Common.Dtos.AttendeeDTOs;
using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [Route("get-all-events")]
        [HttpGet]
        [ProducesResponseType(typeof(List<EventReadDTO>), StatusCodes.Status200OK)]
        public ActionResult GetAllEvents()
        {
            var events = _service.GetAll();

            if (events.Count < 1)
                return BadRequest();

            return Ok(events);
        }

        [Route("get-event-by-id")]
        [HttpGet]
        [ProducesResponseType(typeof(EventReadDTO), StatusCodes.Status200OK)]
        public ActionResult GetEventById(int id)
        {
            var eve = _service.GetById(id);

            if (eve == null)
                return BadRequest();

            return Ok(eve);
        }

        [Route("get-attendees-in-event")]
        [HttpGet]
        [ProducesResponseType(typeof(List<AttendeeReadDTO>), StatusCodes.Status200OK)]
        public ActionResult GetAttendeesInEvent(int id)
        {
            var eve = _service.GetById(id);

            if (eve == null)
                return BadRequest();

            return Ok(eve.Attendees);
        }

        [Route("get-available-attendees-in-event")]
        [HttpGet]
        [ProducesResponseType(typeof(List<AttendeeReadDTO>), StatusCodes.Status200OK)]
        public ActionResult GetAvailableAttendeesInEvent(int id)
        {
            var attendees = _service.GetAvailableAttendeesInEvent(id);

            if (attendees == null)
                return NoContent();

            return Ok(attendees);
        }

        [Route("add-attendees-to-event")]
        [HttpPost]
        public ActionResult AddAttendeesToEvent(int eventId, List<int> attendeesId)
        {
            if (eventId == null)
                return BadRequest();

            if (attendeesId == null)
                return BadRequest();

            foreach (var id in attendeesId)
            {
                _service.AddAttendee(eventId, id);
            }

            return Ok();
        }

        [Route("add-all-attendees-to-event")]
        [HttpGet]
        public ActionResult AddAllAttendeesToEvent(int eventId)
        {
            if (eventId == null)
                return BadRequest();

            var attendees = _service.GetAvailableAttendeesInEvent(eventId);

            if (attendees == null)
                return BadRequest();

            foreach (var attendee in attendees)
            {
                _service.AddAttendee(eventId, attendee.ID);
            }

            return Ok();
        }

        [Route("add-new-event")]
        [HttpPost]
        public ActionResult AddNewEvent([FromBody] EventCreateDTO eventDTO)
        {
            bool checkAdd = _service.Add(eventDTO);

            if (!checkAdd)
                return BadRequest();

            return Ok();
        }

        [Route("delete-event-by-id")]
        [HttpDelete]
        public ActionResult DeleteEventById(int id)
        {
            bool checkDelete = _service.Delete(id);

            if (!checkDelete)
                return NotFound();

            return Ok();
        }

        [Route("remove-attendee-from-event")]
        [HttpDelete]
        public ActionResult RemoveAttendeesFromEvent(int eventId, int attendeeId)
        {
            if (eventId == null)
                return BadRequest();

            if (attendeeId == null)
                return BadRequest();

            _service.RemoveAttendee(eventId, attendeeId);

            return Ok();
        }

        [Route("update-event-by-id")]
        [HttpPut]
        public ActionResult UpdateEventById([FromBody] EventUpdateDTO newEvent, int id)
        {
            bool checkUpdate = _service.Update(newEvent, id);

            if (!checkUpdate)
                return BadRequest();

            return Ok();
        }
    }
}