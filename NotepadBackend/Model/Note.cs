using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NotepadBackend.Model
{
    public class Note
    {
        public long NoteId { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [Required] 
        public string Content { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        
        public static Note CreateNote(User user)
        {
            Note newNote = new Note
            {
                Name = "",
                CreationDateTime = DateTime.Now,
                Content = "",
                User = user
            };

            return newNote;
        }
    }
}
