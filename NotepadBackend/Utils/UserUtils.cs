using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NotepadBackend.Utils
{
    public static class UserUtils
    {
        public static long GetUserIdFromRequest(HttpContext context)
        {
            ClaimsIdentity identity = context.User.Identity as ClaimsIdentity;
            long userId = long.Parse(identity.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.NameIdentifier).Value);

            return userId;
        }
    }
}