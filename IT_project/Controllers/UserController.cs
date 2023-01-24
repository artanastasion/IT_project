using domain.Data.Models;
using domain.Data.Interfaces;
using IT_Project.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserInteractor _users;

        public UserController(UserInteractor users)
        {
            _users = users;
        }

        [Authorize]
        [HttpGet("getUserByLogin")]
        public ActionResult<UserSerializer> GetUserByLogin(string login)
        {
            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан логин");

            var user = _users.GetUserByLogin(login);
            if (user.isFailure)
                return Problem(statusCode: 404, detail: user.Error);

            return Ok(new UserSerializer
            {
                Id = user.Value.Id,
                username = user.Value.UserName,
                PhoneNumber = user.Value.PhoneNumber,
                Fullname = user.Value.Fullname,
                Role = user.Value.Role,
            });
        }
    }
}
