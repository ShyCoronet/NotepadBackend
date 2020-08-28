using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotepadBackend.Model.Repository
{
    public interface INoteRepository
    {
        IQueryable<Note> Notes { get; }
        void AddNote(long userId, Note note);
        void UpdateNote(long userId, Note updatedNote);
        void DeleteNote(long userId, long noteId);
        Note GetNote(long userId, long noteId);
        IEnumerable GetNotes(long userId);
    }
}