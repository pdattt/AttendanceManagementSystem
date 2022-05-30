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

            return Ok(user);
        }
    }
}