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
        public long CreationDateTimeInSeconds { get; set; }
        public string Content { get; set; }
        
        [JsonIgnore]
        public long UserId { get; set; }
        
        public static Note CreateNote()
        {
            DateTime now = DateTime.Now;
            long nowInSeconds = Convert.ToInt64(
                now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            
            Note newNote = new Note
            {
                Name = "",
                CreationDateTime = now,
                CreationDateTimeInSeconds = nowInSeconds,
                Content = "",
            };

            return newNote;
        }
    }
}
