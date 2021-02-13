using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.UI.MVC.Models
{
    public class UserPageViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        //List<Task> dsds

    }
}
/*
 Scaffold-DbContext -Project TaskTracker.Infrastructure.Data
 Scaffold-DbContext "Server=.;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
 */ 