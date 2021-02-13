using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface IUsersSpecialtiesRepository
    {
        IEnumerable<UserSpecialty> GetUserSpecialties();
        UserSpecialty GetUserSpecialtyById(int userSpecialtyId);
        int Create(UserSpecialty userSpecialty);
    }
}