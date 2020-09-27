using System;
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
            accessUser.RefreshTokenData = refreshTokenData;
            _repository.UpdateUser(accessUser);

            return Json(new
            {
                accessToken = accessTokenData.Value,
                deathTime = accessTokenData.DeathTime,
                refreshToken = refreshTokenData.Value
            });
        }

        [HttpPost("refresh_token")]
        public IActionResult RefreshTokens([FromBody]string pastRefreshToken)
        {
            User user = _repository.TryGetUserWithTokenByToken(pastRefreshToken);
            
            if (user == null 
                || CheckLifeTimeRefreshToken(user.RefreshTokenData.DeathTime)) 
                return new BadRequestResult();

            var accessTokenData = _jwtService.GenerateAccessTokenData(user);
            var newRefreshTokenData = _jwtService.GenerateRefreshTokenData();
            user.RefreshTokenData = newRefreshTokenData;
            _repository.UpdateUser(user);

            return Json(new
            {
                accessToken = accessTokenData.Value,
                deathTime = accessTokenData.DeathTime,
                refreshToken = newRefreshTokenData.Value
            });
        }
        
        private bool CheckLifeTimeRefreshToken(DateTime refreshTokenDeathTime)
        {
            bool isAlive = !(refreshTokenDeathTime >= DateTime.UtcNow);

            return isAlive;
        }
    }
}
