using System;
using Newtonsoft.Json;


namespace NotepadBackend.Model
{
    public class Note
    {
        public long NoteId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Content { get; set; }
        
        [JsonIgnore]
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
