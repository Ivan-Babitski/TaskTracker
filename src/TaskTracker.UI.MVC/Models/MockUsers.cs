using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.UI.MVC.Models
{
    public static class MockUsers
    {
        public static List<User> Users { get; set; }
        static MockUsers()
        {
            Users = new List<User> {
                new User{ Id="1", Name="SANJA", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="2", Name="VANJA", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="3", Name="VALJA", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="4", Name="OLEG", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="5", Name="OLe", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="6", Name="Oleole", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="7", Name="PETJA", Login="dfs@xgs", Productivity=2.222f},

                new User{ Id="8", Name="SANJA", Login="dfs@xgs", Productivity=2.222f},

                };
        }
        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Login { get; set; }
            public float Productivity { get; set; }
        }
    };
}
