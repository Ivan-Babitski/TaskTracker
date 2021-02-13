using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface IProcessesRepository
    {
        IEnumerable<User> GetParticipantsByTaskId(int taskIdd);
        Process GetProcessById(int processId);
        IEnumerable<Process> GetProcessesByTaskId(int taskId);
        bool DeleteProcessesByTaskId(int taskId);
        bool DeleteProcessById(int processId);
        int Create(Process process);
        bool Delete(int taskId, string userId);
    }
}