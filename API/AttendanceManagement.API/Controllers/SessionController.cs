using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Common.Dtos.EventDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using AttendanceManagement.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AttendanceManagement.API.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [Route("generate-class-session")]
        [HttpGet]
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

        [Route("generate-event-session")]
        [HttpGet]
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

        [Route("check-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckIn(string cardId, string location)
        {
            var checkIn = await _sessionService.CheckIn(cardId, location);

            if (!checkIn)
                return BadRequest();

            return Ok();
        }

        [Route("get-all-semester-ids")]
        [HttpGet]
        public ActionResult GetAllSemesterIds()
        {
            var ids = _sessionService.GetAllSemesterIds();

            if (ids == null)
                return NotFound();

            return Ok(ids);
        }

        [Route("get-all-classes-and-events-in-semester")]
        [HttpGet]
        public ActionResult GetAllSemesterIds(string semesterId, string type)
        {
            var ids = _sessionService.GetAllInSemester(semesterId, type);

            if (ids == null)
                return NotFound();

            return Ok(ids);
        }

        [Route("get-all-attendance-sessions")]
        [HttpGet]
        public ActionResult GetAllAttendanceSession(string semesterId, string type, string cls_eve_id)
        {
            var sessions = _sessionService.GetAllAttendanceSession(semesterId, type, cls_eve_id);

            if (sessions == null)
                return BadRequest();

            return Ok(sessions);
        }

        [Route("get-all-check-ins")]
        [HttpGet]
        public ActionResult GetAllCheckInsInSession(string semesterId, string type, string cls_eve_id, string date)
        {
            var checkIns = _sessionService.GetAllCheckInsInSession(semesterId, type, cls_eve_id, date);

            if (checkIns == null)
                return BadRequest();

            return Ok(checkIns);
        }

        [Route("get-check-in-by-card-id")]
        [HttpGet]
        public ActionResult GetCheckInByCardId(string semesterId, string type, string cls_eve_id, string date, string cardId)
        {
            var checkIn = _sessionService.GetCheckInByCardId(semesterId, type, cls_eve_id, date, cardId);

            if (checkIn == null)
                return Ok(JsonConvert.SerializeObject("None"));

            return Ok(JsonConvert.SerializeObject(checkIn.Time));
        }

        [AllowAnonymous]
        [Route("count-check-ins-in-semester")]
        [HttpGet]
        public ActionResult CountCheckInsInSemerter(string semesterId, string type, string cls_eve_id)
        {
            var countList = _sessionService.CountCheckInsInSemerter(semesterId, type, cls_eve_id);

            return Ok(countList);
        }
    }
}