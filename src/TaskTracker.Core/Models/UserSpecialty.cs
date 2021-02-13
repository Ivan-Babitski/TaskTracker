namespace TaskTracker.Core.Models
{
    public partial class UserSpecialty
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual User User { get; set; }
    }
}
