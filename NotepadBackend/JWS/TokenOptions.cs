using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace NotepadBackend.JWS
{
    public class TokenOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "NoteAppClient";
        private const string Key = "viefmbldgrmndsbjsrk349456nvsk378gopsjmg349";
        public const int LifeTime = 15;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
