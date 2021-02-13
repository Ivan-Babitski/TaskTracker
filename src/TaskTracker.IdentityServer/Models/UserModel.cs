using System.Collections.Generic;

namespace TaskTracker.IdentityServer.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserRole { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }
        public IList<Role> Roles { get; set; }
        public IList<UserModel> Users { get; set; }
    }
}