namespace TaskTracker.Application.Interfaces
{
    public interface IAccessService
    {
        bool CheckCreaterRights(int taskId, string currentUserId);
        bool CheckVisibilityRights(int taskId, string currentUserId);
    }
}