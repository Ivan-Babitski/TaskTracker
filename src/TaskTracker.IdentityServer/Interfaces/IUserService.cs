using TaskTracker.IdentityServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTracker.IdentityServer.Interfaces
{
    public interface IUserService 
    {
        Task AddUserAsync(UserModel user);
        Task AddUserAsync(RegistrationViewModel user);
        List<UserModel> GetUsers();
    }
}