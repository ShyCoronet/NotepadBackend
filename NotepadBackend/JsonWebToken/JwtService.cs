﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using NotepadBackend.Model;

namespace NotepadBackend.JWS
{
    public class JwtService : IJwtService
    {
        /// <summary>
        /// Returns a new user AccessToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AccessToken GenerateAccessTokenData(User user)
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime expires = utcNow.AddMinutes(TokenConfiguration.AccessLifeTimeInMinutes);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: TokenConfiguration.Issuer,
                audience: TokenConfiguration.Audience,
                notBefore: utcNow,
                claims: GetIdentity(user).Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    TokenConfiguration.GetSymmetricSecurityKey(), 
                    SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
   
            
            return new AccessToken
            {
                Value = encodedJwt,
                DeathTime = expires
            };
        }

        /// <summary>
        /// Returns a new user RefreshToken
        /// </summary>
        /// <returns></returns>
        public RefreshToken GenerateRefreshTokenData()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime expires = utcNow.AddDays(TokenConfiguration.RefreshLifeTimeInDays);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: TokenConfiguration.Issuer,
                audience: TokenConfiguration.Audience,
                notBefore: utcNow,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    TokenConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new RefreshToken
            {
                Value = encodedJwt,
                DeathTime = expires
            };
        }

        /// <summary>
        /// Returns a user claims identity containing user ID and user role 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private ClaimsIdentity GetIdentity(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "GetToken", ClaimTypes.NameIdentifier,
                ClaimTypes.Role);

            return claimsIdentity;
        }
    }
}