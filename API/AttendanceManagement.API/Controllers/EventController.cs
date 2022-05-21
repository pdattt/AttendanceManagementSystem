using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [Route("/get-all-events")]
        [HttpGet]
        public ActionResult GetAllEvents()
        {
            List<EventReadDTO> events = _service.GetAll();

            if (events.Count > 0)
                return Ok(events);

            return BadRequest();
        }

        [Route("/get-by-id")]
        [HttpGet]
        public ActionResult GetEventById(int id)
        {
            EventReadDTO eve = _service.GetById(id);

            if (eve != null)
                return Ok(eve);

            return BadRequest();
        }

        [Route("/add-new-event")]
        [HttpPost]
        public ActionResult AddNewEvent([FromBody] EventCreateDTO eventDTO)
        {
            bool checkAdd = _service.Add(eventDTO);

            if (!checkAdd)
                return BadRequest();

            return Ok();
        }

        [Route("/delete-event-by-id")]
        [HttpDelete]
        public ActionResult DeleteEventById(int id)
        {
            bool checkDelete = _service.Delete(id);

            if (!checkDelete)
                return BadRequest();

            return Ok();
        }

        [Route("/update-event-by-id")]
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