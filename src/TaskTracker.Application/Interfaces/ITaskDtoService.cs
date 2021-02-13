using System.Collections.Generic;
using TaskTracker.Application.Models;

namespace TaskTracker.Application.Interfaces
{
    public interface ITaskDtoService
    {
        /// <summary>
        /// Method check if Task is in visible for current user.
        /// </summary>
        /// <returns>TaskDto instans or null if access denied of task does not exist or DB error.</returns>
        TaskDto GetTaskById(int taskId, string currentUserId);
        /// <summary>
        /// Method check if Task is in visible for current user.
        /// </summary>
        /// <returns>Users which is participants of current task.</returns>
        IEnumerable<UserDto> GetParticipantsByTaskId(int taskId, string currentUserId);
        IEnumerable<TaskDto> GetChildrenTasksbyTaskId(int taskId, string currentUserId);
        IEnumerable<PriorityDto> GetListPriorities();
        int AddTask(TaskDto newTask);
        /// <summary>
        /// Remove user from Process if 'currentUserId' is creater of this task or already is in process.
        /// </summary>
        /// <returns>Returning status boolean variable independent is it result of access result or database operation result.</returns>
        bool RemoveExecutor(int taskId, string removeUserId, string currentUserId);
        bool AppointExecutor(int taskId, string executorUserId, string currentUserId);
        IEnumerable<TaskDto> GetTasksByCreaterId(int taskId, string userId, string currentUserId);
        IEnumerable<TaskDto> GetAllVisibleTasks(string currentUserId);
        bool CloseTask(int taskId, string currentUserId);
        bool DeleteTask(int taskId, string currentUserId);
        bool ReOpenTask(int taskId, string currentUserId);
    }
}