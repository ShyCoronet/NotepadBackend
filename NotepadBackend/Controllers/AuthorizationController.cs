using Microsoft.AspNetCore.Mvc;
using NotepadBackend.JWS;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthorizationController(IUserRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("token")]
        public IActionResult GetTokens([FromBody] User user)
        {
            User accessUser = _repository.
                GetUserByAuthorizationData(user.Login, user.Password);

            if (accessUser == null) return new BadRequestResult();

            string accessToken = _jwtService.GenerateAccessToken(accessUser);
            string refreshToken = _jwtService.GenerateRefreshToken();
            accessUser.RefreshToken = refreshToken;
            _repository.UpdateUser(accessUser);
            HttpContext.Response.Cookies.Append("refreshToken", refreshToken);

            return Json(accessToken);
        }

        [HttpGet("token")]
        public IActionResult RefreshTokens()
        {
            string pastRefreshToken = HttpContext.Request.Cookies?["refreshToken"];
            User user = _repository.GetUserByToken(pastRefreshToken);
            
            if (user == null) return new BadRequestResult();

            string accessToken = _jwtService.GenerateAccessToken(user);
            string newRefreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _repository.UpdateUser(user);
            HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken);
            
            return Json(accessToken);
        }
    }
}
