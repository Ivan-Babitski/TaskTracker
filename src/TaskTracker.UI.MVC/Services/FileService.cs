using AutoMapper;
using System;
using System.IO;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Models;
using TaskTracker.UI.MVC.Interfaces;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Services
{
    public class FileService : IFileService
    {
        private IMapper _mapper;
        private IFileDtoService _fileDtoServise;
        public FileService(IMapper mapper, IFileDtoService fileDtoServise)
        {
            _mapper = mapper;
            _fileDtoServise = fileDtoServise;
        }

        public FileModel GetFile(int fileId, string currentUserId)
        {
            var model = _fileDtoServise.Download(fileId, currentUserId);
            if (model == null)
            {
                return default;
            }


            FileModel file = new FileModel
            {
                Id = model.Id,
                FileName = model.Name,
                FileType = model.FileType,
                DataFiles = model.DataFiles,
                ContentType = model.ContentType
            };

            return file;
        }

        public bool SendFile(TaskViewModel model, string currentUserId)
        {

            if (model.File != null)
            {
                if (model.File.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(model.File.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension

                    FileDto fileDto = new FileDto
                    {
                        TaskId = model.Id,
                        Name = fileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        model.File.CopyTo(target);
                        fileDto.DataFiles = target.ToArray();
                    }

                    return _fileDtoServise.Upload(fileDto, currentUserId);
                }
                return false;
            }
            return false;
        }

        public void DeleteFile(int fileId, string currentUserId)
        {
            _fileDtoServise.Delete(fileId, currentUserId);
        }
    }
}