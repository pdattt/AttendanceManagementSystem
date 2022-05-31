using AttendanceManagement.Common.Dtos.UserDTOs;
using AttendanceManagement.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(UserLoginDTO userLogin)
        {
            var user = _service.Authentication(userLogin);

            if (user == null)
                return BadRequest("User is not found");

            var token = _service.GenerateToken(user);

            return Ok(token);
        }

        [Route("get-user")]
        [HttpGet]
        public ActionResult GetUser([FromHeader] string token)
        {
            var user = _service.DecodeToken(token);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
    }
}