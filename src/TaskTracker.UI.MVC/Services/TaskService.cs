using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Application.Enums;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Models;
using TaskTracker.UI.MVC.Interfaces;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Services
{
    public class TaskService : ITaskService
    {
        private IMapper _mapper;
        private ITaskDtoService _taskDtoService;
        private ITagDtoService _tagDtoService;
        private IAccountDtoService _accountDtoService;
        private IFileDtoService _fileDtoServise;
        public TaskService(
            IMapper mapper,
            ITaskDtoService taskDtoService, 
            ITagDtoService tagDtoService,
            IAccountDtoService accountDtoService,
            IFileDtoService fileDtoService)
        {
            _mapper = mapper;
            _taskDtoService = taskDtoService;
            _tagDtoService = tagDtoService;
            _accountDtoService = accountDtoService;
            _fileDtoServise = fileDtoService;
        }

        public TaskCreateViewModel PrepareViewModel(string currentUserId)
        {
            var model = _taskDtoService.GetListPriorities();
            if (model == null)
            {
                return default;
            }
            var mapper = _mapper.Map<IEnumerable<SelectListItem>>(model);

            var result = new TaskCreateViewModel
            {
                Priorities = mapper.ToList()
            };

            var listVisibleTasks = _taskDtoService.GetAllVisibleTasks(currentUserId);
            if (listVisibleTasks == null)
            {
                return default;
            }
            result.Tasks = _mapper.Map<IEnumerable<SelectListItem>>(listVisibleTasks).ToList();

            return result;
        }

        public int CreateNewTask(TaskCreateViewModel newTask)
        {
            var model = _mapper.Map<TaskDto>(newTask);
            int returnTaskId = _taskDtoService.AddTask(model);
            if (returnTaskId < 0)
            {
                return returnTaskId;
            }

            return returnTaskId;
        }

        public TaskViewModel GetTaskById(int taskId, string currentUserId)
        {
            var model = _taskDtoService.GetTaskById(taskId, currentUserId);

            if (model == null)
            {
                return default;
            }
            var result = _mapper.Map<TaskViewModel>(model);

            var listTagDtos = _tagDtoService.GetTagsByTaskId(taskId, currentUserId);
            if (listTagDtos == null)
            {
                return default;
            }
            result.Tags = _mapper.Map<IEnumerable<TagModel>>(listTagDtos).ToList();

            var listParticipantsDtos = _taskDtoService.GetParticipantsByTaskId(taskId, currentUserId);
            if (listParticipantsDtos == null)
            {
                return default;
            }
            result.Participants = _mapper.Map<IEnumerable<UserModel>>(listParticipantsDtos).ToList();

            var listFriendDtos = _accountDtoService.GetFriendsByUserId(currentUserId);
            if (listFriendDtos == null)
            {
                return default;
            }
            result.Participants = _mapper.Map<IEnumerable<UserModel>>(listFriendDtos).ToList();

            var listFileDtos = _fileDtoServise.GetFilesByTaskId(taskId, currentUserId);
            if (listFileDtos == null)
            {
                return default;
            }
            result.Files = _mapper.Map<IEnumerable<FileModel>>(listFileDtos).ToList();

            var listChildrenTaskDtos = _taskDtoService.GetChildrenTasksbyTaskId(taskId, currentUserId);
            if (listChildrenTaskDtos == null)
            {
                return default;
            }
            result.ChildrenTasks = _mapper.Map<IEnumerable<TaskModel>>(listChildrenTaskDtos).ToList();

            if (result.CreaterId == currentUserId)
            {
                result.IsOwner = true;
            }

            foreach (var item in result.Participants)
            {
                if (item.Id == currentUserId)
                {
                    result.IsJoined = true;
                }
            }

            result.Priority = ((Priorities)model.PriorityId).ToString();

            return result;
        }

        public List<TaskViewModel> GetAllTasks(string currentUserId)
        {
            var model = _taskDtoService.GetAllVisibleTasks(currentUserId);
            if (model == null)
            {
                return default;
            }
            return _mapper.Map<List<TaskViewModel>>(model);
        }


        public bool RemoveExecutor(int taskId, string removeUserId, string currentUserId)
        {
            return _taskDtoService.RemoveExecutor(taskId, removeUserId, currentUserId);
        }

        public bool AppointExecutor(TaskViewModel task, string currentUserId)
        {
            return _taskDtoService.AppointExecutor(task.Id, task.NewExecutiorId, currentUserId);
        }

        public bool CloseTask(int taskId, string currentUserId)
        {
            return _taskDtoService.CloseTask(taskId, currentUserId);
        }

        public bool DeleteTask(int taskId, string currentUserId)
        {
            return _taskDtoService.DeleteTask(taskId, currentUserId);
        }

        public bool ReOpenTask(int taskId, string currentUserId)
        {
            return _taskDtoService.ReOpenTask(taskId, currentUserId);
        }
    }
}