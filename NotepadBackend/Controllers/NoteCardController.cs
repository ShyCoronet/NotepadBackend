using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotepadBackend.Model.Note;
using NotepadBackend.Model.Repository;

namespace NotepadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteCardController
    {
        private INoteRepository _repository { get; set; }

        public NoteCardController(INoteRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IEnumerable<NoteCard> ViewAllCards()
        {
            return _repository.NoteCards;
        }
    }
}
