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
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }
        
        public static Note CreateNote()
        {
            Note newNote = new Note
            {
                Name = "",
                CreationDateTime = DateTime.Now,
                Content = "",
            };

            return newNote;
        }
    }
}
