using System;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public RegistrationController(IUserRepository repository)
        {
            _repository = repository;
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
                RegistrationDateTime = DateTime.Now
            };
            
            _repository.AddUser(newUser);

            return Ok();
        }
    }
}
