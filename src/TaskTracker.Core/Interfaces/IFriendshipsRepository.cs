using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface IFriendshipsRepository
    {
        IEnumerable<Friendship> GetFriendships();
        Friendship GetFriendshipById(int friendshipId);
        int Create(Friendship friendship);
        IEnumerable<User> GetFriendsByUserId(string userId);
        bool DeleteFriendById(int friendshipId);
        bool DeleteAllFriendsByUserId(string userId);
    }
}