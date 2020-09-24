using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model;
using NotepadBackend.Model.Exceptions;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        
        [HttpGet("notes")]
        public IEnumerable<Note> GetNotes()
        {
            IEnumerable<Note> notes = _noteRepository
                .GetNotes(GetUserIdFromRequest());
            return notes;
        }

        [HttpPost("note")]
        public Note CreateNote()
        {
            Note newNote = Note.CreateNote();
            _noteRepository.AddNote(GetUserIdFromRequest(), newNote);
            return newNote;
        }

        [HttpPut("note")]
        public Note UpdateNote([FromBody] Note updatedNote)
        {
            _noteRepository.UpdateNote(GetUserIdFromRequest(), updatedNote);
            return updatedNote;
        }

        [HttpDelete("note")]
        public long DeleteNote([FromBody] long noteId)
        {
            _noteRepository.DeleteNote(GetUserIdFromRequest(), noteId);
            return noteId;
        }

        private long GetUserIdFromRequest()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            long userId = long.Parse(identity.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.NameIdentifier).Value);

            return userId;
        }
    }
}
