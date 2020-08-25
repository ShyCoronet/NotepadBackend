using System.Collections.Generic;
using System.Linq;

namespace NotepadBackend.Model.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
