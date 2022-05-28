using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ISessionService _sessionService;
        private readonly IEventService _eventService;

        public SessionController(IClassService classService, ISessionService sessionService, IEventService eventService)
        {
            _classService = classService;
            _sessionService = sessionService;
            _eventService = eventService;
        }

        [Route("/generate-class-session")]
        [HttpPost]
        public ActionResult GenerateClassSession(int id)
        {
            ClassReadDTO cls = _classService.GetById(id);

            if (cls == null)
                return BadRequest();

            bool checkGenerate = _sessionService.GenerateClassSession(cls);

            if (!checkGenerate)
                return BadRequest();

            return Ok();
        }

        [Route("/generate-event-session")]
        [HttpPost]
        public ActionResult GenerateEventSession(int id)
        {
            EventReadDTO eve = _eventService.GetById(id);

            if (eve == null)
                return BadRequest();

            bool checkGenerate = _sessionService.GenerateEventSession(eve);

            if (!checkGenerate)
                return BadRequest();

            return Ok();
        }
    }
}