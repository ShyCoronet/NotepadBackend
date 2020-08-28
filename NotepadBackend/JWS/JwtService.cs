using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotepadBackend.Model;

namespace NotepadBackend.JWS
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration) =>
            _configuration = configuration;

        public string GenerateAccessToken(User user)
        {
            DateTime utcNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: TokenConfiguration.Issuer,
                audience: TokenConfiguration.Audience,
                notBefore: utcNow,
                claims: GetIdentity(user).Claims,
                expires: utcNow.Add(TimeSpan.FromMinutes(TokenConfiguration.AccessLifeTime)),
                signingCredentials: new SigningCredentials(
                    TokenConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public string GenerateRefreshToken()
        {
            DateTime utcNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: TokenConfiguration.Issuer,
                audience: TokenConfiguration.Audience,
                notBefore: utcNow,
                expires: utcNow.Add(TimeSpan.FromMinutes(TokenConfiguration.RefreshLifeTime)),
                signingCredentials: new SigningCredentials(
                    TokenConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
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