using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotepadBackend.Model;

namespace NotepadBackend.Model.Repository
{
    public interface INoteRepository
    {
        IQueryable<Note> Notes { get; }
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(Note note);
    }
}
