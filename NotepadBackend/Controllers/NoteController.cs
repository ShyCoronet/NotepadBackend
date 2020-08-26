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
    [ApiController]
    public class NoteController
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        
        [HttpGet("notes")]
        public IEnumerable<Note> GetNotes([FromBody] long userId)
        {
            return _noteRepository.GetNotes(userId);
        }

        [HttpPost("note")]
        public Note CreateNote([FromBody] long userId)
        {
            Note newNote = Note.CreateNote();
            _noteRepository.AddNote(userId, newNote);
            return newNote;
        }

        [HttpPut("note")]
        public Note UpdateNote([FromBody] Note updatedNote)
        {
            _noteRepository.UpdateNote(updatedNote);
            return updatedNote;
        }

        [HttpDelete("note")]
        public void DeleteNote([FromBody] long noteId)
        {
            _noteRepository.DeleteNote(noteId);
        }
    }
}
