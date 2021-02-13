using System.Collections.Generic;

namespace TaskTracker.Core.Models
{
    public partial class Tag
    {
        public Tag()
        {
            TasksTags = new HashSet<TaskTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskTag> TasksTags { get; set; }
    }
}
