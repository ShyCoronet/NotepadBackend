using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public void UpdateUser(User updatedUser)
        {
            User originalUser = _context.Users.Find(updatedUser.UserId);
            originalUser.Login = updatedUser.Login;
            originalUser.Email = updatedUser.Email;
            originalUser.Password = updatedUser.Password;
            _context.SaveChanges();
        }

        public void DeleteUser(long userId)
        {
            User deletedUser = GetUserById(userId);
            _context.Users.Remove(deletedUser);
            _context.SaveChanges();
        }

        public User GetUserById(long userId)
        {
            return _context.Users.Find(userId);
        }
        
        public User GetUserByAuthorizationData(string login, string password)
        {
            return _context.Users.FirstOrDefault(user => 
                user.Login == login && user.Password == password);
        }
    }
}
