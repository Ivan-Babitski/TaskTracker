using Microsoft.AspNetCore.StaticFiles;
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
    public class FileDtoService : IFileDtoService
    {
        private IFilesRepository _filesRepository;
        private ITaskDtoService _taskDtoService;
        private IAccessService _accessService;

        public FileDtoService(
            IFilesRepository filesRepository,
            ITaskDtoService taskDtoService,
            IAccessService accessService)
        {
            _filesRepository = filesRepository;
            _taskDtoService = taskDtoService;
            _accessService = accessService;
        }
        public bool Upload(FileDto fileDto, string currentUserId)
        {
            if (!_accessService.CheckCreaterRights(fileDto.TaskId, currentUserId))
            {
                return false;
            }
            try
            {
                var model = ObjectMapper.Mapper.Map<File>(fileDto);
                _filesRepository.Create(model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public FileDto Download(int fileId, string currentUserId)
        {
            try
            {
                var file = _filesRepository.GetFileById(fileId);
                if (!_accessService.CheckVisibilityRights(file.TaskId, currentUserId))
                {
                    return default;
                }
                var model = _filesRepository.GetFileById(fileId);
                var result = ObjectMapper.Mapper.Map<FileDto>(model);

                string contentType;

                var fileProvider = new FileExtensionContentTypeProvider();
                //Figures out what the content type should be based on the file name.
                if (!fileProvider.TryGetContentType(file.Name, out contentType))
                {
                    throw new ArgumentOutOfRangeException($"Unable to find Content Type for file name {file.Name}.");
                }
                if (contentType == null)
                {
                    return default;
                }
                //result.ContentType = "application/octet-stream";
                result.ContentType = contentType;
                return result;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public bool Delete(int fileId, string currentUserId)
        {
            try
            {
                var file = _filesRepository.GetFileById(fileId);
                if (!_accessService.CheckCreaterRights(file.TaskId, currentUserId))
                {
                    return false;
                }
                return _filesRepository.DeleteFileById(fileId);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<FileDto> GetFilesByTaskId(int taskId, string currentUserId)
        {
            if (!_accessService.CheckVisibilityRights(taskId, currentUserId))
            {
                return default;
            }
            try
            {
                var model = _filesRepository.GetFilesByTaskId(taskId).ToList();
                return ObjectMapper.Mapper.Map<IEnumerable<FileDto>>(model);
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}