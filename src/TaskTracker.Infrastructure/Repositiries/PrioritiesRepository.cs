using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class PrioritiesRepository : IPrioritiesRepository
    {
        private readonly TaskTrackerContext _context;
        public PrioritiesRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Priority> GetPriorities()
        {
            foreach (var item in _context.Priorities)
            {
                yield return item;
            }
        }

        public Priority GetPriorityById(int priorityId)
        {
            if (priorityId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Priorities.FirstOrDefault(p => p.Id == priorityId);
        }

        public int Create(Priority priority)
        {
            if (priority is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Priorities.Add(priority);
            _context.SaveChanges();
            return priority.Id;
        }
    }
}