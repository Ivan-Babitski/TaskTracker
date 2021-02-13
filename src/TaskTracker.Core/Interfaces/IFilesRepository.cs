using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface IFilesRepository
    {
        IEnumerable<File> GetFiles();
        File GetFileById(int fileId);
        IEnumerable<File> GetFilesByTaskId(int taskId);
        int Create(File file);
        bool DeleteFileById(int fileId);
        bool DeleteAllFilesByTaskId(int taskId);
    }
}