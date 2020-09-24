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
                TryGetUserByAuthorizationData(user.Login, user.Password);

            if (accessUser == null) return new BadRequestResult();

            var accessTokenData = _jwtService.GenerateAccessTokenData(accessUser);
            var refreshTokenData = _jwtService.GenerateRefreshTokenData();
            accessUser.RefreshToken = refreshTokenData.Token;
            _repository.UpdateUser(accessUser);

            return Json(new
            {
                accessToken = accessTokenData.Token,
                lifeTimeInSeconds = accessTokenData.LifeTimeInSeconds,
                refreshToken = refreshTokenData.Token
            });
        }

        [HttpPost("refresh_token")]
        public IActionResult RefreshTokens([FromBody]string pastRefreshToken)
        {
            User user = _repository.GetUserByToken(pastRefreshToken);
            
            if (user == null) return new BadRequestResult();

            var accessTokenData = _jwtService.GenerateAccessTokenData(user);
            var newRefreshToken = _jwtService.GenerateRefreshTokenData();
            user.RefreshToken = newRefreshToken.Token;
            _repository.UpdateUser(user);

            return Json(new
            {
                accessToken = accessTokenData.Token,
                lifeTimeInSeconds = accessTokenData.LifeTimeInSeconds,
                refreshToken = newRefreshToken.Token
            });
        }
    }
}
