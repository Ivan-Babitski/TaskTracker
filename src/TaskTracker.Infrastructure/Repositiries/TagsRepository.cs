using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly TaskTrackerContext _context;
        public TagsRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetTags()
        {
            foreach (var item in _context.Tags)
            {
                yield return item;
            }
        }

        public Tag GetTagById(int tagId)
        {
            if (tagId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Tags.FirstOrDefault(p => p.Id == tagId);
        }

        public IEnumerable<Tag> GetTagsByTaskId(int taskId)
        {
            if (taskId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            var tagsId = _context.TasksTags.Where(p => p.TaskId == taskId).Select(p => p.Id);

            foreach (var item in _context.Tags.Where(p => tagsId.Contains(p.Id)))
            {
                yield return item;
            }
        }

        public int Create(Tag tag)
        {
            if (tag is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag.Id;
        }
    }
}