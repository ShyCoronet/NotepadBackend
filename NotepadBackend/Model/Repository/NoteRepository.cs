using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NotepadBackend.Model.Exceptions;

namespace NotepadBackend.Model.Repository
{
    public class NoteRepository : INoteRepository
    {
        public IQueryable<Note> Notes => _context.Notes;

        private readonly DataContext _context;

        public NoteRepository(DataContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Adds a new User Note to the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        public void AddNote(long userId, Note note)
        {
            note.UserId = userId;
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the User's Note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updatedNote"></param>
        /// <exception cref="IncorrectUserDataException"></exception>
        public void UpdateNote(long userId, Note updatedNote)
        {
            Note originalNote = GetNote(userId, updatedNote.NoteId);

            if (originalNote == null)
                throw new IncorrectUserDataException(
                    "Incorrect user ID or note ID",
                    new {userId, updatedNote.NoteId});
            
            originalNote.Name = updatedNote.Name;
            originalNote.Content = updatedNote.Content;
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the User's Note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <exception cref="IncorrectUserDataException"></exception>
        public void DeleteNote(long userId, long noteId)
        {
            Note deletedNote = GetNote(userId, noteId);
            
            if (deletedNote == null)
                throw new IncorrectUserDataException(
                    "Incorrect user ID or note ID",
                    new {userId, noteId});
            
            _context.Notes.Remove(deletedNote);
            _context.SaveChanges();
        }
        
        /// <summary>
        /// Returns a Note by Note ID and User ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectUserDataException"></exception>
        public Note GetNote(long userId, long noteId)
        {
            Note note = _context.Notes.FirstOrDefault(
                n => n.UserId == userId && n.NoteId == noteId);
            
            if (note == null)
                throw new IncorrectUserDataException(
                    "Incorrect user ID or note ID",
                    new {userId, noteId});

            return note;
        }

        /// <summary>
        /// Returns a Sequence of User Notes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectUserDataException"></exception>
        public IEnumerable<Note> GetNotes(long userId)
        {
            IEnumerable<Note> notes = _context.Notes
                .Where(n => n.UserId == userId);
            
            if (notes == null) 
                throw new IncorrectUserDataException(
                    "Incorrect user ID", userId);

            return notes;
        }
    }
}