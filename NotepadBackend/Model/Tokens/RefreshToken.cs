using System;

namespace NotepadBackend.Model
{
    public class RefreshToken
    {
        public long TokenId { get; set; }
        public string Value { get; set; }
        public DateTime DeathTime { get; set; }
    }
}