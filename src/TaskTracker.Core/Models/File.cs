using System;

namespace TaskTracker.Core.Models
{
    public partial class File
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual Task Task { get; set; }
    }
}