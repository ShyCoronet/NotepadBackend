using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NotepadBackend.JWS
{
    public static class TokenConfiguration
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "NoteAppClient";
        private const string Key = "viefmbldgrmndsbjsrk349456nvsk378gopsjmg349";
        public const int AccessLifeTime = 60;
        public const int RefreshLifeTime = 43800;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
