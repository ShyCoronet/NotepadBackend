using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model.Note;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class NoteCardController
    {
        private INoteRepository _repository { get; }

        public NoteCardController(INoteRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("notes")]
        public IEnumerable<NoteCard> ViewAllNotes()
        {
            return _repository.NoteCards;
        }

        [HttpPost("note")]
        public NoteCard CreateNote([FromBody] NoteCard card)
        {
            NoteCard newNote = new NoteCard()
            {
                Id = _repository.NoteCards.Count,
                Name = card.Name,
                CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                ViewTime = card.ViewTime,
                TextContent = card.TextContent
            };

            _repository.Add(newNote);

            return newNote;
        }

        [HttpPut("note")]
        public NoteCard UpdateNote([FromBody] NoteCard card)
        {
            return _repository.Update(card);
        }
    }
}
