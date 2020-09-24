using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.JWS;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public RegistrationController(IUserRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        
        [HttpPost("sign_up")]
        public IActionResult RegistrationUser([FromBody] User user)
        {
            if (!LoginCheckForUniqueness(user.Login))
            {
                return new BadRequestResult();
            }

            if (!EmailCheckForUniqueness(user.Email))
            {
                return new BadRequestResult();
            }
   
            User newUser = new User
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Role = "user",
                RegistrationDateTime = DateTime.Now,
                RefreshToken = _jwtService.GenerateRefreshTokenData().Token
            };
            
            _repository.AddUser(newUser);

            return Ok();
        }

        private bool LoginCheckForUniqueness(string login)
        {
            User userByLogin = _repository.TryGetUserByLogin(login);

            return userByLogin == null;
        }

        private bool EmailCheckForUniqueness(string email)
        {
            User userByEmail = _repository.TryGetUserByEmail(email);

            return userByEmail == null;
        }
    }
}
