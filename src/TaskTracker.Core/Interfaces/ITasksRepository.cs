using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface ITasksRepository
    {
        IEnumerable<Task> GetChildrenTasksByTaskId(int taskId);
        IEnumerable<Task> GetTasks();
        IEnumerable<Task> GetTasksByUserId(string userId);
        Task GetTaskById(int taskId);
        int Create(Task task);
        IEnumerable<Task> GetTasksByCreaterId(string userId);
        void Update(Task task);
        void Delete(int taskId);
    }
}