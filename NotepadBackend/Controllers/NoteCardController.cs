using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model.Note;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api")]
    [ApiController]
    public class NoteCardController
    {
        private INoteRepository _repository { get; set; }

        public NoteCardController(INoteRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("notes")]
        public IEnumerable<NoteCard> ViewAllCards()
        {
            return _repository.NoteCards;
        }

        [HttpPost("note")]
        public void CreateCard([FromBody] NoteCard card)
        {
            _repository.Add(new NoteCard
            {
                Id = _repository.NoteCards.Count,
                Name = card.Name,
                CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                Summary = card.Summary
            });
        }
    }
}
