using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult RegistrationUser([FromBody] UserForRegistration user)
        {
            User newUser = new User
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Role = "user",
                RegistrationDateTime = DateTime.Now
            };

            try
            {
                _repository.AddUser(newUser);
                return StatusCode(200);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(400);
            }
        }
    }
}
