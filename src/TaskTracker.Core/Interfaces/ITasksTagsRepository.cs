using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface ITasksTagsRepository
    {
        IEnumerable<TaskTag> GetTasksTags();
        TaskTag GetTaskTagById(int taskTagId);
        int Create(TaskTag taskTag);
        bool DeleteTaskTagById(int taskTagId);
        bool DeleteAllTasksTagsByTaskId(int taskId);
    }
}