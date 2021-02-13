using System;

namespace TaskTracker.UI.MVC.Models
{
    public class FilterDueDate
    {
       public bool CheckDueDate(DateTime date, double subtractDay)
        {
            var nowDate = DateTime.Now;
            var subtractTodoDate = date.AddDays(-subtractDay);
            if (subtractTodoDate <= nowDate && date.Date >= nowDate.Date)
            {
                return true;
            }
            return false;
        }
    }
}