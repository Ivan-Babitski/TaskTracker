using System.Collections.Generic;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Interfaces
{
    public interface ITaskService
    {
        TaskCreateViewModel PrepareViewModel(string currentUserId);
        int CreateNewTask(TaskCreateViewModel newTask);
        TaskViewModel GetTaskById(int taskId, string currentUserId);
        List<TaskViewModel> GetAllTasks(string currentUserId);
        bool RemoveExecutor(int taskId, string removeUserId, string currentUserId);
        bool AppointExecutor(TaskViewModel task, string currentUserId);
        bool CloseTask(int taskId, string currentUserId);
        bool DeleteTask(int taskId, string currentUserId);
        bool ReOpenTask(int taskId, string currentUserId);
    }
}