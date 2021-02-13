using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        private readonly TaskTrackerContext _context;
        public FilesRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<File> GetFiles()
        {
            foreach (var item in _context.Files)
            {
                yield return item;
            }
        }

        public File GetFileById(int fileId)
        {
            if (fileId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Files.FirstOrDefault(p => p.Id == fileId);
        }

        public IEnumerable<File> GetFilesByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            foreach (var item in _context.Files.Where(f => f.TaskId == taskId))
            {
                yield return item;
            }
        }

        public int Create(File file)
        {
            if (file is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Files.Add(file);
            _context.SaveChanges();
            return file.Id;
        }

        public bool DeleteFileById(int fileId)
        {
            if (fileId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var file = _context.Files.SingleOrDefault(f => f.Id == fileId);

            if (file == null)
            {
                return true;
            }

            _context.Files.Remove(file);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteAllFilesByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var files = _context.Files.Where(f => f.TaskId == taskId);

            if (files.Any())
            {
                _context.Files.RemoveRange(files);
                _context.SaveChanges();
            }

            return true;
        }
    }
}