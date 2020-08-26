using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

        [HttpPost("test")]
        public async Task<IActionResult> Test([FromBody] User user)
        {
            User currentUser = await _repository.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

            return Json(new
            {
                currentUser.UserId,
                currentUser.Login,
                currentUser.Role
            });
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody] UserForAuthorization user)
        {
            var identity = GetIdentity(user.Login, user.Password);
            if (identity == null) return new BadRequestResult();
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(TokenOptions.LifeTime)),
                signingCredentials: new SigningCredentials(TokenOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name,
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            User user = _repository.Users.FirstOrDefault(user => user.Login == login && user.Password == password);   

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "GetToken", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            
            return claimsIdentity;
        }
    }
}
