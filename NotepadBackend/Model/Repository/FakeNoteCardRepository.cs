using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotepadBackend.Model.Note;

namespace NotepadBackend.Model.Repository
{
    public class FakeNoteCardRepository : INoteRepository

    {
        public IQueryable<NoteCard> NoteCards { get; set; }

        public FakeNoteCardRepository()
        {
            NoteCards = new EnumerableQuery<NoteCard>(
                new List<NoteCard>
                {
                    new NoteCard()
                    {
                        Id = 0,
                        Name = "Список покупок",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        Summary = "Молоко"
                    },

                    new NoteCard()
                    {
                        Id = 1,
                        Name = "Стихотоврение",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        Summary = "Я помню чудное мнгновенье"
                    },

                    new NoteCard()
                    {
                        Id = 2,
                        Name = "Стихотоврение",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        Summary = "Я помню чудное мнгновенье"
                    }, 

                    new NoteCard()
                    {
                        Id = 3,
                        Name = "Стихотоврение",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        Summary = "Я помню чудное мнгновенье"
                    }
                });
        }
    }
}
