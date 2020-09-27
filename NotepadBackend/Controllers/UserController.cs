using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.JWS;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;
using NotepadBackend.Utils;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        
        [Authorize]
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
                user.RegistrationDateTime,
            });
        }
        
        [HttpPost("sign_up")]
        public IActionResult SignUpUser([FromBody] User user)
        {
            if (!LoginCheckForUniqueness(user.Login) 
                || !EmailCheckForUniqueness(user.Email))
                return new BadRequestResult();

            User newUser = new User
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                RegistrationDateTime = DateTime.Now
            };
            
            _userRepository.AddUser(newUser);

            return Ok();
        }
        
        private bool LoginCheckForUniqueness(string login)
        {
            User userByLogin = _userRepository.TryGetUserByLogin(login);

            return userByLogin == null;
        }

        private bool EmailCheckForUniqueness(string email)
        {
            User userByEmail = _userRepository.TryGetUserByEmail(email);

            return userByEmail == null;
        }
        
    }
}