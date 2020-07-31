using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NotepadBackend.Model.Note
{
    public class NoteCard
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreateTime { get; set; }
        public string ViewTime { get; set; }
        public string Summary { get; set; }
        public string TextContent { get; set; }
    }
}
