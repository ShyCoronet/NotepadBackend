using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NotepadBackend.JWS
{
    public static class TokenConfiguration
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "NoteAppClient";
        private const string Key = "viefmbldgrmndsbjsrk349456nvsk378gopsjmg349";
        public const int AccessLifeTime = 5;
        public const int RefreshLifeTime = 15;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
