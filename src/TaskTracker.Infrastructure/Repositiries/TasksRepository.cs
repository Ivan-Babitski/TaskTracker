using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TaskTrackerContext _context;
        public TasksRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetTasks()
        {
            foreach (var item in _context.Tasks)
            {
                yield return item;
            }
        }

        public IEnumerable<Task> GetTasksByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new System.ArgumentNullException();
            }

            var processesId = _context.Processes.Where(p => p.UserId == userId).Select(p => p.Id);

            foreach (var item in _context.Tasks.Where(p => processesId.Contains(p.Id)))
            {
                yield return item;
            }
        }

        public Task GetTaskById(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Tasks.FirstOrDefault(p => p.Id == taskId);
        }

        public int Create(Task task)
        {
            if (task is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task.Id;
        }

        public IEnumerable<Task> GetChildrenTasksByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            foreach (var item in _context.Tasks.Where(f => f.ParentTaskId == taskId))
            {
                yield return item;
            }
        }

        public IEnumerable<Task> GetTasksByCreaterId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new System.ArgumentNullException();
            }

            foreach (var item in _context.Tasks.Where(t => t.CreaterId == userId))
            {
                yield return item;
            }
        }

        public void Update(Task task)
        {
            if (task is null)
            {
                throw new ArgumentNullException();
            }

            var oldTask = _context.Tasks.SingleOrDefault(t => t.Id == task.Id);

            if (oldTask != null)
            {
                _context.Entry(oldTask).CurrentValues.SetValues(task);
            }
        }

        public void Delete(int taskId)
        {
            if (taskId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}