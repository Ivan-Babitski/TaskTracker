using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Application.Models;

namespace TaskTracker.Application.Interfaces
{
    public interface IFileDtoService
    {
        /// <summary>
        /// Uploads a file only if current user is a creator of current task.
        /// </summary>
        /// <returns>Returning status boolean variable independent is it result of access result or database operation result.</returns>
        bool Upload(FileDto fileDto, string currentUserId);
        /// <summary>
        /// Method checks if Task is in visible for current user and if Task contains needs file.
        /// </summary>
        /// <returns>Returning a file that needs or 'Exception' independent is it result of access result or database operation result.</returns>
        FileDto Download(int fileId, string currentUserId);
        /// <summary>
        /// Method check if Task has been created by current user.
        /// </summary>
        /// <returns>Returning status boolean variable independent is it result of access result or database operation result.</returns>
        bool Delete(int fileId, string currentUserId);
        /// <summary>
        /// Method check if Task is in visible for current user.
        /// </summary>
        /// <returns>IEnumerable<FileDto> or exeption.</returns>
        IEnumerable<FileDto> GetFilesByTaskId(int taskId, string currentUserId);
    }
}