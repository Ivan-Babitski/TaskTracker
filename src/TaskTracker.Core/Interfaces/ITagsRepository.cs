using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface ITagsRepository
    {
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int tagId);
        IEnumerable<Tag> GetTagsByTaskId(int taskId);
        int Create(Tag tag);
    }
}