using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(string userId);
        string Create(User user);
    }
}