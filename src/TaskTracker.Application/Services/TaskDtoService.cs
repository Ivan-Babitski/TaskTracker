using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Mapper;
using TaskTracker.Application.Models;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Application.Services
{
    public class TaskDtoService : ITaskDtoService
    {
        private IMapper _mapper;
        private IPrioritiesRepository _prioritiesRepository;
        private ITasksRepository _tasksRepository;
        private IAccountDtoService _accountDtoService;
        private ITagDtoService _tagDtoService;
        private IProcessesRepository _processesRepository;
        private IAccessService _accessService;

        public TaskDtoService(
            IMapper mapper,
            IPrioritiesRepository prioritiesRepository, 
            ITasksRepository tasksRepository,
            IAccountDtoService accountDtoService,
            ITagDtoService tagDtoService,
            IProcessesRepository processesRepository,
            IAccessService accessService)
        {
            _mapper = mapper;
            _prioritiesRepository = prioritiesRepository;
            _tasksRepository = tasksRepository;
            _accountDtoService = accountDtoService;
            _tagDtoService = tagDtoService;
            _processesRepository = processesRepository;
            _accessService = accessService;
        }
        public TaskDto GetTaskById(int taskId, string currentUserId)
        {
            if (!_accessService.CheckVisibilityRights(taskId, currentUserId))
            {
                return default;
            }
            try
            {
                var model = _tasksRepository.GetTaskById(taskId);
                return ObjectMapper.Mapper.Map<TaskDto>(model);
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public IEnumerable<UserDto> GetParticipantsByTaskId(int taskId, string currentUserId)
        {
            if (!_accessService.CheckVisibilityRights(taskId, currentUserId))
            {
                return default;
            }
            try
            {
                var model = _processesRepository.GetParticipantsByTaskId(taskId);
                return ObjectMapper.Mapper.Map<IEnumerable<UserDto>>(model);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public IEnumerable<TaskDto> GetChildrenTasksbyTaskId(int taskId, string currentUserId)
        {
            if (!_accessService.CheckVisibilityRights(taskId, currentUserId))
            {
                return default;
            }
            try
            {
                var model = _tasksRepository.GetChildrenTasksByTaskId(taskId);
                return ObjectMapper.Mapper.Map<IEnumerable<TaskDto>>(model);
            }
            catch (Exception e)
            {
                return default;
            }
        }
        public IEnumerable<PriorityDto> GetListPriorities()
        {
            try
            {
                var model = _prioritiesRepository.GetPriorities();
                return ObjectMapper.Mapper.Map<IEnumerable<PriorityDto>>(model);
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public int AddTask(TaskDto newTask)
        {
            if (newTask.ParentTaskId == 0)
            {
                newTask.ParentTaskId = null;
            }
            var listTags = _tagDtoService.AddAndReturnTags(newTask.Tags);

            try
            {
                var model = ObjectMapper.Mapper.Map<Task>(newTask);
                model.CreateDate = DateTime.Now;
                int newTaskId = _tasksRepository.Create(model);
                foreach (var item in listTags)
                {
                    _tagDtoService.AddTagToTask(new TaskTagDto { TagId = item.Id, TaskId = newTaskId });
                }
                return newTaskId;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public bool RemoveExecutor(int taskId, string removeUserId, string currentUserId)
        {
            try
            {
                bool flag = false;
                var listParticipants = _processesRepository.GetParticipantsByTaskId(taskId).ToList();
                foreach (var user in listParticipants)
                {
                    if (user.Id == currentUserId)
                    {
                        flag = true;
                    }
                }
                if (_accessService.CheckCreaterRights(taskId, currentUserId) || flag)
                {
                    return _processesRepository.Delete(taskId, removeUserId);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AppointExecutor(int taskId, string executorUserId, string currentUserId)
        {
            if (!_accessService.CheckCreaterRights(taskId, currentUserId))
            {
                return false;
            }

            try
            {
                _ = _processesRepository.Create(new Process
                {
                    TaskId = taskId,
                    UserId = executorUserId,
                    JoinDate = DateTime.Now
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<TaskDto> GetTasksByCreaterId(int taskId, string userId, string currentUserId)
        {
            if (!_accessService.CheckVisibilityRights(taskId, currentUserId))
            {
                return default;
            }
            try
            {
                var model = _tasksRepository.GetTasksByCreaterId(userId);
                return ObjectMapper.Mapper.Map<IEnumerable<TaskDto>>(model);
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public IEnumerable<TaskDto> GetAllVisibleTasks(string currentUserId)
        {
            try
            {
                var friends = _accountDtoService.GetFriendsByUserId(currentUserId).ToList();
                friends.Add(new UserDto { Id = currentUserId } );
                List<Task> tasks = new List<Task>();
                foreach (var friend in friends)
                {
                    foreach (var task in _tasksRepository.GetTasksByCreaterId(friend.Id))
                    {
                        tasks.Add(task);
                    }
                }
                return ObjectMapper.Mapper.Map<List<TaskDto>>(tasks);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public bool CloseTask(int taskId, string currentUserId)
        {
            if (!_accessService.CheckCreaterRights(taskId, currentUserId))
            {
                return false;
            }
            try
            {
                var model = _tasksRepository.GetTaskById(taskId);
                model.CloseDate = DateTime.Now;
                _tasksRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTask(int taskId, string currentUserId)
        {
            if (!_accessService.CheckCreaterRights(taskId, currentUserId))
            {
                return false;
            }
            try
            {
                _tasksRepository.Delete(taskId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReOpenTask(int taskId, string currentUserId)
        {
            if (!_accessService.CheckCreaterRights(taskId, currentUserId))
            {
                return false;
            }
            try
            {
                var model = _tasksRepository.GetTaskById(taskId);
                model.ReOpenDate = DateTime.Now;
                _tasksRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}