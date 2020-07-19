using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotepadBackend.Model.Note
{
    public class NoteCard
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreateTime { get; set; }
        public string Summary { get; set; }
    }
}
