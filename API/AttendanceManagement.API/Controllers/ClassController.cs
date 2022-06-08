﻿using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassController(IClassService service)
        {
            _service = service;
        }

        [Route("get-all-classes")]
        [HttpGet]
        public ActionResult GetAllClasses()
        {
            var classes = _service.GetAll();

            if (classes.Count < 1)
                return BadRequest();

            foreach (var cls in classes)
            {
                cls.DaysOfWeek = _service.GetDaysOfWeek(cls.DaysOfWeek);
            }

            return Ok(classes);
        }

        [Route("get-class-by-id")]
        [HttpGet]
        public ActionResult GetClassById(int id)
        {
            var cls = _service.GetById(id);

            if (cls == null)
                return NotFound();

            cls.DaysOfWeek = _service.GetDaysOfWeek(cls.DaysOfWeek);

            return Ok(cls);
        }

        [Route("get-attendees-in-class")]
        [HttpGet]
        public ActionResult GetAttendeesInClass(int id)
        {
            var cls = _service.GetById(id);

            if (cls == null)
                return BadRequest();

            return Ok(cls.Attendees);
        }

        [Route("get-available-attendees-in-class")]
        [HttpGet]
        public ActionResult GetAvailableAttendeesInClass(int id)
        {
            var attendees = _service.GetAvailableAttendeesInClass(id);

            if (attendees == null)
                return NoContent();

            return Ok(attendees);
        }

        [Route("get-days-of-week")]
        [HttpGet]
        public ActionResult GetDaysOfWeek(int id)
        {
            if (id == null)
                return BadRequest("Id is null");

            var cls = _service.GetById(id);

            if (cls == null)
                return BadRequest("Class not found");

            string daysOfWeek = _service.GetDaysOfWeek(cls.DaysOfWeek);

            return Ok(daysOfWeek);
        }

        [Route("add-attendees-to-class")]
        [HttpPost]
        public ActionResult AddAttendeesToClass(int classId, List<int> attendeesId)
        {
            if (classId == null)
                return BadRequest();

            if (attendeesId == null)
                return BadRequest();

            foreach (var id in attendeesId)
            {
                _service.AddAttendee(classId, id);
            }

            return Ok();
        }

        [Route("add-new-class")]
        [HttpPost]
        public ActionResult AddNewClass([FromBody] ClassCreateDTO newclass)
        {
            bool checkAdd = _service.Add(newclass);

            if (!checkAdd)
                return BadRequest();

            return Ok();
        }

        [Route("delete-class-by-id")]
        [HttpDelete]
        public ActionResult DeleteClassById(int id)
        {
            bool checkDelete = _service.Delete(id);

            if (!checkDelete)
                return NotFound();

            return Ok();
        }

        [Route("remove-attendee-from-class")]
        [HttpDelete]
        public ActionResult RemoveAttendeesFromEvent(int classId, int attendeeId)
        {
            if (classId == null)
                return BadRequest();

            if (attendeeId == null)
                return BadRequest();

            _service.RemoveAttendee(classId, attendeeId);

            return Ok();
        }

        [Route("update-class-by-id")]
        [HttpPut]
        public ActionResult UpdateClassById([FromBody] ClassUpdateDTO newClass, int id)
        {
            bool checkUpdate = _service.Update(newClass, id);

            if (!checkUpdate)
                return BadRequest();

            return Ok();
        }
    }
}