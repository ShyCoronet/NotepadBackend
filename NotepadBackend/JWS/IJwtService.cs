using NotepadBackend.Model;

namespace NotepadBackend.JWS
{
    public interface IJwtService
    {
        public AccessToken GenerateAccessTokenData(User user);
        public RefreshToken GenerateRefreshTokenData();
    }
}