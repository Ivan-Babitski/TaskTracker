using TaskTracker.IdentityServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTracker.IdentityServer.Interfaces
{ 
    public interface IRoleService
    {
        Task<IList<string>> GetUserRolesAsync();
        Task AddRoleToUserAsync(string username, string roles);
        Task<IList<Role>> GetAllRoles();
    }
}