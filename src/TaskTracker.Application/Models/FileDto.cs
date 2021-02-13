using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Application.Models
{
    public class FileDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ContentType { get; set; }
    }
}
