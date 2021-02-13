using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface ISpecialtiesRepository
    {
        IEnumerable<Specialty> GetSpecialties();
        Specialty GetSpecialtyById(int specialtyId);
        int Create(Specialty specialty);
    }
}