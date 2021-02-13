using System.Collections.Generic;
using TaskTracker.Application.Models;

namespace TaskTracker.Application.Interfaces
{
    public interface ITagDtoService
    {
        IEnumerable<TagDto> AddAndReturnTags(string tags);
        IEnumerable<TagDto> GetTags();
        IEnumerable<TagDto> GetTagsByTaskId(int taskId, string currentUserId);
        void AddTagToTask(TaskTagDto taskTagDto);
    }
}