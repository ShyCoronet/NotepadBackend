namespace NotepadBackend.Model
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long LifeTimeInSeconds { get; set; }
    }
}