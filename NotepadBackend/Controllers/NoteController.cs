using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class NoteController
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;

        public NoteController(INoteRepository noteRepository, IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }
        
        [HttpGet("notes")]
        public IEnumerable<Note> GetAllNotes([FromBody] string login)
        {
            var notes = _noteRepository.Notes.Where(note => note.User.Login == login);
            return notes;
        }

        [HttpPost("note")]
        public object CreateNote([FromBody] string login)
        {
            User user = _userRepository.Users.FirstOrDefault(user => user.Login == login);
            Note newNote = Note.CreateNote(user);
            _noteRepository.AddNote(newNote);
            
            return new
            {
                newNote.NoteId,
                newNote.Name,
                newNote.Content,
                CreationTime = newNote.CreationDateTime
            };
        }

        [HttpPut("note")]
        public Note UpdateNote([FromBody] Note note)
        {
            Note updateNote = _noteRepository.Notes.FirstOrDefault(n => n.NoteId == note.NoteId);
            updateNote.Name = note.Name;
            updateNote.Content = note.Content;
            _noteRepository.UpdateNote(updateNote);
            return updateNote;
        }
    }
}
