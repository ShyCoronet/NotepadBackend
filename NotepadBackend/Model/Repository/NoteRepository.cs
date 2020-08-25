using System.Linq;

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
            
        public void AddNote(Note note)
        {
            _context.Add(note);
            _context.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            _context.Update(note);
            _context.SaveChanges();
        }

        public void DeleteNote(Note note)
        {
            _context.Remove(note);
            _context.SaveChanges();
        }
    }
}