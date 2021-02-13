using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class ProcessesRepository : IProcessesRepository
    {
        private readonly TaskTrackerContext _context;
        public ProcessesRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Process> GetProcess()
        {
            foreach (var item in _context.Processes)
            {
                yield return item;
            }
        }

        public Process GetProcessById(int processId)
        {
            if (processId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Processes.FirstOrDefault(p => p.Id == processId);
        }

        public IEnumerable<User> GetParticipantsByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var usersId = _context.Processes.Where(p => p.TaskId == taskId).Select(p => p.UserId);

            foreach (var item in _context.Users.Where(p => usersId.Contains(p.Id)))
            {
                yield return item;
            }
        }

        public IEnumerable<Process> GetProcessesByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            foreach (var item in _context.Processes.Where(p => p.TaskId == taskId))
            {
                yield return item;
            }
        }

        public bool DeleteProcessesByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var processes = _context.Processes.Where(p => p.TaskId == taskId);

            if (processes.Any())
            {
                _context.Processes.RemoveRange(processes);
                _context.SaveChanges();
            }

            return true;
        }

        public bool DeleteProcessById(int processId)
        {
            if (processId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var process = _context.Processes.SingleOrDefault(p => p.Id == processId);

            if (process == null)
            {
                return true;
            }

            _context.Processes.Remove(process);
            _context.SaveChanges();

            return true;
        }

        public int Create(Process process)
        {
            if (process is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Processes.Add(process);
            _context.SaveChanges();
            return process.Id;
        }

        public bool Delete(int taskId, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}