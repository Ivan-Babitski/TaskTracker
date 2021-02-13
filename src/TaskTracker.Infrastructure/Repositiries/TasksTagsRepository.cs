using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class TasksTagsRepository : ITasksTagsRepository
    {
        private readonly TaskTrackerContext _context;
        public TasksTagsRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskTag> GetTasksTags()
        {
            foreach (var item in _context.TasksTags)
            {
                yield return item;
            }
        }

        public TaskTag GetTaskTagById(int taskTagId)
        {
            if (taskTagId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.TasksTags.FirstOrDefault(p => p.Id == taskTagId);
        }

        public int Create(TaskTag taskTag)
        {
            if (taskTag is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.TasksTags.Add(taskTag);
            _context.SaveChanges();
            return taskTag.Id;
        }

        public bool DeleteTaskTagById(int taskTagId)
        {
            if (taskTagId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var taskTag = _context.TasksTags.SingleOrDefault(t => t.Id == taskTagId);

            if (taskTag == null)
            {
                return true;
            }

            _context.TasksTags.Remove(taskTag);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteAllTasksTagsByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var tasksTags = _context.TasksTags.Where(t => t.TaskId == taskId);

            if (tasksTags.Any())
            {
                _context.TasksTags.RemoveRange(tasksTags);
                _context.SaveChanges();
            }

            return true;
        }
    }
}