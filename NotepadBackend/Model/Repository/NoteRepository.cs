using System.Collections.Generic;
using System.Linq;

namespace NotepadBackend.Model.Repository
{
    public class NoteRepository : INoteRepository
    {
        public IQueryable<Note> Notes { get; }

        private DataContext _context;

        public NoteRepository(DataContext context)
        {
            _context = context;
        }
        public void AddNote(long userId, Note note)
        {
            note.UserId = userId;
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void UpdateNote(Note updatedNote)
        {
            Note originalNote = GetNote(updatedNote.NoteId);
            originalNote.Name = updatedNote.Name;
            originalNote.Content = updatedNote.Content;
            _context.SaveChanges();
        }

        public void DeleteNote(long noteId)
        {
            Note deletedNote = GetNote(noteId);
            _context.Notes.Remove(deletedNote);
            _context.SaveChanges();
        }
        
        public Note GetNote(long noteId)
        {
            return _context.Notes.Find(noteId);
        }

        public IEnumerable<Note> GetNotes(long userId)
        {
            return _context.Notes.Where(n => n.UserId == userId);
        }
    }
}