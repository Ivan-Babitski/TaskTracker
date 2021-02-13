using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly TaskTrackerContext _context;
        public UsersRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            foreach (var item in _context.Users)
            {
                yield return item;
            }
        }

        public User GetUserById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new System.ArgumentNullException();
            }

            return _context.Users.FirstOrDefault(p => p.Id == userId);
        }

        public string Create(User user)
        {
            if (user is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
    }
}