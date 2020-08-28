using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotepadBackend.Model.Repository
{
    public class NoteRepository : INoteRepository
    {
        public IQueryable<Note> Notes { get; }

        private readonly DataContext _context;

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

        public void UpdateNote(long userId, Note updatedNote)
        {
            Note originalNote = GetNote(userId, updatedNote.NoteId);
            
            if (originalNote != null)
            {
                originalNote.Name = updatedNote.Name;
                originalNote.Content = updatedNote.Content;
                _context.SaveChanges();
            }
        }

        public void DeleteNote(long userId, long noteId)
        {
            Note deletedNote = GetNote(userId, noteId);

            if (deletedNote != null)
            {
                _context.Notes.Remove(deletedNote);
                _context.SaveChanges();
            }
        }
        
        public Note GetNote(long userId, long noteId)
        {
            return _context.Notes.FirstOrDefault(
                n => n.UserId == userId && n.NoteId == noteId);
        }

        public IEnumerable GetNotes(long userId)
        {
            return _context.Notes
                .Where(n => n.UserId == userId)
                .Select(n => new
                {
                    n.NoteId,
                    n.Name,
                    n.Content,
                    n.CreationDateTime
                });
        }
    }
}