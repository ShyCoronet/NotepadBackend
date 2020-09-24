using System.Linq;
using NotepadBackend.Model.Exceptions;

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
        
        /// <summary>
        /// Adds a new User to the database
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates the data of the User
        /// </summary>
        /// <param name="updatedUser"></param>
        public void UpdateUser(User updatedUser)
        {
            User originalUser = GetUserById(updatedUser.UserId);
            originalUser.Login = updatedUser.Login;
            originalUser.Email = updatedUser.Email;
            originalUser.Password = updatedUser.Password;
            originalUser.RefreshToken = updatedUser.RefreshToken;
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes a User from the database
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="IncorrectUserDataException"></exception>
        public void DeleteUser(long userId)
        {
            User deletedUser = GetUserById(userId);
            
            if (deletedUser == null)
                throw new IncorrectUserDataException(
                    "Incorrect user ID", userId);

            _context.Users.Remove(deletedUser);
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns a User from the database by ID 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectUserDataException"></exception>
        public User GetUserById(long userId)
        {
            User user = _context.Users.Find(userId);
            
            if (user == null)
                throw new IncorrectUserDataException(
                    "Incorrect user ID", userId);

            return user;
        }
        
        /// <summary>
        /// Returns the User from the database by login and password, otherwise returns null
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User TryGetUserByAuthorizationData(string login, string password)
        {
            return _context.Users.FirstOrDefault(u => 
                u.Login == login && u.Password == password);
        }

        /// <summary>
        /// Returns the User from the database by the RefreshToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="IncorrectUserDataException"></exception>
        public User GetUserByToken(string token)
        {
            User user = _context.Users.FirstOrDefault(u => u.RefreshToken == token);
            
            if (user == null)
                throw new IncorrectUserDataException(
                    "Invalid refresh token", token);

            return user;
        }

        /// <summary>
        /// Returns the User from the database by login, otherwise returns null
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public User TryGetUserByLogin(string login)
        {
            User user = _context.Users.FirstOrDefault(u => u.Login == login);

            return user;
        }

        /// <summary>
        /// Returns the User from the database by email, otherwise returns null
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User TryGetUserByEmail(string email)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);

            return user;
        }
    }
}
