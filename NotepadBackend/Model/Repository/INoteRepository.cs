using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotepadBackend.Model.Note;

namespace NotepadBackend.Model.Repository
{
    public interface INoteRepository
    {
        List<NoteCard> NoteCards { get; set; }

        void Add(NoteCard card);

        NoteCard Update(NoteCard card);

    }
}
