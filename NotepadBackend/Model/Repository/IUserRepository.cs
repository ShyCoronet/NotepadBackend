using System.Linq;

namespace NotepadBackend.Model.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void AddUser(User user);
        void UpdateUser(User updatedUser);
        void DeleteUser(long userId);
        User TryGetUserByAuthorizationData(string login, string password);
        User GetUserById(long id);
        User GetUserByToken(string token);
        User TryGetUserByLogin(string login);
        User TryGetUserByEmail(string email);
    }
}
