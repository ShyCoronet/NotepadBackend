using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NotepadBackend.Model
{
    public class User
    {
        public long UserId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string Login { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(350)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string Role { get; set; }

        [Required]
        public DateTime RegistrationDateTime { get; set; }
        
        public List<Note> Notes { get; set; }
    }
}
