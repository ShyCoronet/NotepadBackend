using System;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.JWS;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public RegistrationController(IUserRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        
        [HttpPost("registration")]
        public IActionResult RegistrationUser([FromBody] User user)
        {
            User newUser = new User
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Role = "user",
                RegistrationDateTime = DateTime.Now,
                RefreshToken = _jwtService.GenerateRefreshToken()
            };
            
            _repository.AddUser(newUser);

            return Ok();
        }
    }
}
