using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotepadBackend.Model.Note;

namespace NotepadBackend.Model.Repository
{
    public interface INoteRepository
    {
        IQueryable<NoteCard> NoteCards { get; set; }

    }
}
