using NotepadBackend.Model;

namespace NotepadBackend.JWS
{
    public interface IJwtService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
    }
}