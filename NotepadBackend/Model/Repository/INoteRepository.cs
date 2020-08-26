using System.Collections.Generic;
using System.Linq;

namespace NotepadBackend.Model.Repository
{
    public interface INoteRepository
    {
        IQueryable<Note> Notes { get; }
        void AddNote(long userId, Note note);
        void UpdateNote(Note updatedNote);
        void DeleteNote(long noteId);
        Note GetNote(long noteId);
        IEnumerable<Note> GetNotes(long userId);
    }
}