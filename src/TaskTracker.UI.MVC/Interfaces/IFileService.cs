using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Interfaces
{
    public interface IFileService
    {
        FileModel GetFile(int fileId, string currentUserId);
        bool SendFile(TaskViewModel model, string currentUserId);
        void DeleteFile(int fileId, string currentUserId);
    }
}