using System.Collections.Generic;
using TaskTracker.Application.Models;

namespace TaskTracker.Application.Interfaces
{
    public interface IAccountDtoService
    {
        IEnumerable<UserDto> GetFriendsByUserId(string currentUserId);
    }
}