using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotepadBackend.Model
{
    public class User
    {
        public long UserId { get; set; }
        public string Login { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        
        [JsonIgnore]
        public RefreshToken RefreshTokenData { get; set; }
        
        [JsonIgnore]
        public IEnumerable<Note> Notes { get; set; }
    }
}
