using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotepadBackend.Model.Note;

namespace NotepadBackend.Model.Repository
{
    public class FakeNoteCardRepository : INoteRepository

    {
        public List<NoteCard> NoteCards { get; set; }

        public FakeNoteCardRepository()
        {
            NoteCards = new List<NoteCard>
                {
                    new NoteCard()
                    {
                        Id = 0,
                        Name = "Список покупок",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        ViewTime = "Только что",
                        Summary = "Молоко",
                        TextContent = "Молоко\nХлеб\nБичпакет"
                    },

                    new NoteCard()
                    {
                        Id = 1,
                        Name = "Стихотворение",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        ViewTime = "Только что",
                        Summary = "Я помню чудное мнгновенье",
                        TextContent = "Я помню чудное мнгновенье\nПередо мной явилась ты"
                    },

                    new NoteCard()
                    {
                        Id = 2,
                        Name = "Стихотворение",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        ViewTime = "Только что",
                        Summary = "Я помню чудное мнгновенье",
                        TextContent = "Я помню чудное мнгновенье\nПередо мной явилась ты"
                    }, 

                    new NoteCard()
                    {
                        Id = 3,
                        Name = "Стихотворение",
                        CreateTime = DateTime.Now.ToString("yy-MM-dd"),
                        ViewTime = "Только что",
                        Summary = "Я помню чудное мнгновенье",
                        TextContent = "Я помню чудное мнгновенье\nПередо мной явилась ты"
                    }
                };
        }

        public void Add(NoteCard card)
        {
            NoteCards.Insert(0, card);
        }

        public NoteCard Update(NoteCard card)
        {
            int index = NoteCards.IndexOf(NoteCards.FirstOrDefault(note => note.Id == card.Id));
            NoteCards[index] = card;

            return card;
        }
    }
}
