using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface IPrioritiesRepository
    {
        IEnumerable<Priority> GetPriorities();
        Priority GetPriorityById(int priorityId);
        int Create(Priority priority);
    }
}