using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.UI.MVC.Models
{
    public class MockNotification
    {
        public static List<Notification> Notifications { get; set; }
        static MockNotification()
        {
            Notifications = new List<Notification>
            {
                new Notification{
                    Id=1, UserId="SANJA", NotificationValue="dfs@xgssdfdserghrhgygdsg CreateDate=new DateTime(2022, 5, 1), CloseDate=new DateTime(2022, 5, 1), DueDate=new DateTime(2022, 5, 1)" },

                new Notification{ Id=2, UserId="VANJA", NotificationValue="dfs@xgsdsgsdg, CreateDate=new DateTime(2021, 2, 6), CloseDate=new DateTime(2022, 5, 1), DueDate=new DateTime(2022, 5, 1), ReOpenDate=null,Value=value Priority=medium,FileValue=null, ParentTaskId=null , Category=work, Tag=#ToDoList"},

                new Notification{ Id=3, UserId="VALJA", NotificationValue="dfs@xgdsgdsgsdgs, CreateDate=new DateTime(2021, 2, 7), CloseDate=new DateTime(2022, 5, 1), DueDate=new DateTime(2022, 5, 1), ReOpenDate=null, Value=value, Priority=high,FileValue=null, ParentTaskId=null, Category=#Task" },

                new Notification{ Id=4, UserId="OLEG", NotificationValue="dfs@xgasdasfs, CreateDate=new DateTime(2021, 2, 3), CloseDate=new DateTime(2022, 5, 1), DueDate=new DateTime(2022, 5, 1), ReOpenDate=null,,FileValue=null, ParentTaskId=null , Category=#ToDo"},

                new Notification{ Id=5, UserId="OLe", NotificationValue="dfs@xewrewgs, CreateDate=new DateTime(2021, 2, 4), CloseDate=new DateTime(2022, 5, 1), DueDate=new DateTime(2022, 5, 1), ReOpenDate=null,eValue=null, ParentTaskId=null , Category=#TaskTracker"},
            };
        }
    }
}