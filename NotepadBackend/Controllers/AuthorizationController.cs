using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public AuthorizationController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody] User user)
        {
            ClaimsIdentity identity = GetIdentity(user.Login, user.Password);
            
            if (identity == null) return new BadRequestResult();
            
            DateTime utcNow = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: TokenConfigurations.Issuer,
                audience: TokenConfigurations.Audience,
                notBefore: utcNow,
                claims: identity.Claims,
                expires: utcNow.Add(TimeSpan.FromMinutes(TokenConfigurations.LifeTime)),
                signingCredentials: new SigningCredentials(TokenConfigurations.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt
            };
            
            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            User user = _repository.GetUserByAuthorizationData(login, password);

            if (user == null) return null;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "GetToken", ClaimTypes.NameIdentifier,
                ClaimTypes.Role);
            
            return claimsIdentity;
        }
    }
}
