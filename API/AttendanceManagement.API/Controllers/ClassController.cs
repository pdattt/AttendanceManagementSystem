using AttendanceManagement.Common.Dtos.ClassDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassController(IClassService service)
        {
            _service = service;
        }

        [Route("/get-all-classes")]
        [HttpGet]
        public ActionResult GetAllClasses()
        {
            var classes = _service.GetAll();

            if (classes.Count < 1)
                return BadRequest();

            return Ok(classes);
        }

        [Route("/get-class-by-id")]
        [HttpGet]
        public ActionResult GetClassById(int id)
        {
            var cls = _service.GetById(id);

            if (cls == null)
                return NotFound();

            return Ok(cls);
        }

        [Route("/add-new-class")]
        [HttpPost]
        public ActionResult AddNewClass([FromBody] ClassCreateDTO newclass)
        {
            bool checkAdd = _service.Add(newclass);

            if (!checkAdd)
                return BadRequest();

            return Ok();
        }

        [Route("/delete-class-by-id")]
        [HttpDelete]
        public ActionResult DeleteClassById(int id)
        {
            bool checkDelete = _service.Delete(id);

            if (!checkDelete)
                return NotFound();

            return Ok();
        }

        [Route("/update-class-by-id")]
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