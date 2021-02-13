using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Mapper;
using TaskTracker.Application.Models;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Application.Services
{
    public class TagDtoService : ITagDtoService
    {
        private ITagsRepository _tagsRepository;
        private ITasksTagsRepository _tasksTagsRepository;
        private IAccessService _accessService;

        public TagDtoService(
            ITagsRepository tagsRepository,
            IAccessService accessService,
            ITasksTagsRepository tasksTagsRepository)
        {
            _tagsRepository = tagsRepository;
            _accessService = accessService;
            _tasksTagsRepository = tasksTagsRepository;
        }

        public IEnumerable<TagDto> AddAndReturnTags(string tags)
        {
            if (String.IsNullOrEmpty(tags))
            {
                return new List<TagDto>();
            }

            var allTags = GetTags();
            if (allTags == null)
            {
                return new List<TagDto>();
            }

            var parsTags = tags.Replace(" ", "").Split(',');

            List<TagDto> result = new List<TagDto>();//вернём все для таски
            bool flagMatch = false;

            for (int i = 0; i < parsTags.Length; i++)
            {
                foreach (var exTag in allTags)
                {
                    parsTags[i] = parsTags[i].ToUpper();
                    if (parsTags[i] == exTag.Name)
                    {
                        flagMatch = true;
                        result.Add(exTag);
                        break;
                        //вернуть тэг в relult
                    }
                }
                if (!flagMatch)
                {
                    try
                    {
                        int newTagId = _tagsRepository.Create(new Tag { Name = parsTags[i] });
                        result.Add(new TagDto { Id = newTagId, Name = parsTags[i] });
                        flagMatch = false;
                    }
                    catch (Exception e)
                    {
                        return default;
                    }
                }
                flagMatch = false;
            }


            return result;
        }

        public IEnumerable<TagDto> GetTags()
        {
            try
            {
                var model = _tagsRepository.GetTags();
                var result = ObjectMapper.Mapper.Map<IEnumerable<TagDto>>(model);
                return result;
            }
            catch (Exception e)
            {
                return default;
            }
        }


        public void AddTagToTask(TaskTagDto taskTagDto)
        {
            try
            {
                var model = ObjectMapper.Mapper.Map<TaskTag>(taskTagDto);
                _tasksTagsRepository.Create(model);
            }
            catch (Exception e)
            {
                //just loggin ex
            }
        }


        public IEnumerable<TagDto> GetTagsByTaskId(int taskId, string currentUserId)
        {
            if (!_accessService.CheckVisibilityRights(taskId, currentUserId))
            {
                return default;
            }

            try
            {
                var model = _tagsRepository.GetTagsByTaskId(taskId);
                return ObjectMapper.Mapper.Map<IEnumerable<TagDto>>(model);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}