using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotepadBackend.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        public IQueryable<User> Users => _context.Users;
        
        private readonly DataContext _context;
        
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
