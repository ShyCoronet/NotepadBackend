using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NotepadBackend.JWS
{
    public static class TokenConfiguration
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "NoteAppClient";
        private static string Key { get; } = Environment.GetEnvironmentVariable("TokenSecretKey");
        public const int AccessLifeTimeInMinutes = 60;
        public const int RefreshLifeTimeInDays = 30;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
