using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class SpecialtiesRepository : ISpecialtiesRepository
    {
        private readonly TaskTrackerContext _context;
        public SpecialtiesRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Specialty> GetSpecialties()
        {
            foreach (var item in _context.Specialties)
            {
                yield return item;
            }
        }

        public Specialty GetSpecialtyById(int specialtyId)
        {
            if (specialtyId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Specialties.FirstOrDefault(p => p.Id == specialtyId);
        }

        public int Create(Specialty specialty)
        {
            if (specialty is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Specialties.Add(specialty);
            _context.SaveChanges();
            return specialty.Id;
        }
    }
}