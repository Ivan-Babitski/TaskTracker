using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class FriendshipsRepository : IFriendshipsRepository
    {
        private readonly TaskTrackerContext _context;
        public FriendshipsRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Friendship> GetFriendships()
        {
            foreach (var item in _context.Friendships)
            {
                yield return item;
            }
        }

        public Friendship GetFriendshipById(int friendshipId)
        {
            if (friendshipId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Friendships.FirstOrDefault(p => p.Id == friendshipId);
        }

        public int Create(Friendship friendship)
        {
            if (friendship is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Friendships.Add(friendship);
            _context.SaveChanges();
            return friendship.Id;
        }

        public IEnumerable<User> GetFriendsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new System.ArgumentNullException();
            }

            return _context.Friendships.Where(f => f.UserId == userId).Select(f => f.User);
        }

        public bool DeleteFriendById(int friendshipId)
        {
            if (friendshipId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var friendship = _context.Friendships.SingleOrDefault(f => f.Id == friendshipId);

            if (friendship == null)
            {
                return true;
            }

            _context.Friendships.Remove(friendship);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteAllFriendsByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new System.ArgumentNullException();
            }

            var friendships = _context.Friendships.Where(f => f.UserId == userId || f.FriendId == userId);

            if (friendships.Any())
            {
                _context.Friendships.RemoveRange(friendships);
                _context.SaveChanges();
            }

            return true;
        }
    }
}