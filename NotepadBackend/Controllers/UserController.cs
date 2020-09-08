using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;
using NotepadBackend.Utils;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            User user = _userRepository.GetUserById(
                UserUtils.GetUserIdFromRequest(HttpContext));
            
            if (user == null) return new BadRequestResult();

            return Json(new
            {
                user.UserId,
                user.Login,
                user.Email,
                RegistrationDateTime = user.RegistrationDateTime.ToString("MM/dd/yyyy")
            });
        }
    }
}