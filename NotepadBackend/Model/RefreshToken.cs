namespace NotepadBackend.Model
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public long LifeTimeInSeconds { get; set; }
    }
}