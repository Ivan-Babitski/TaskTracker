using System;
using System.Linq;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Models;
using TaskTracker.Core.Interfaces;

namespace TaskTracker.Application.Services
{
    public class AccessService : IAccessService
    {
        private IAccountDtoService _accountDtoService;
        private ITasksRepository _tasksRepository;

        public AccessService(
            ITasksRepository tasksRepository,
            IAccountDtoService accountDtoService)
        {
            _tasksRepository = tasksRepository;
            _accountDtoService = accountDtoService;
        }
        public bool CheckVisibilityRights(int taskId, string currentUserId)
        {
            var listFriendIds = _accountDtoService.GetFriendsByUserId(currentUserId).ToList();
            if (listFriendIds == null)
            {
                return false;
            }

            listFriendIds.Add(new UserDto { Id = currentUserId });

            foreach (var friend in listFriendIds)
            {
                foreach (var task in _tasksRepository.GetTasksByCreaterId(currentUserId))
                {
                    if (task.CreaterId == friend.Id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool CheckCreaterRights(int taskId, string currentUserId)
        {
            try
            {
                var model = _tasksRepository.GetTaskById(taskId);
                if (model.CreaterId == currentUserId)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
