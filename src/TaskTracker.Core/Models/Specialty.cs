using System.Collections.Generic;

namespace TaskTracker.Core.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            UsersSpecialties = new HashSet<UserSpecialty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserSpecialty> UsersSpecialties { get; set; }
    }
}
