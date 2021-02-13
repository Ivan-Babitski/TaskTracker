using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class UsersSpecialtiesRepository : IUsersSpecialtiesRepository
    {
        private readonly TaskTrackerContext _context;
        public UsersSpecialtiesRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<UserSpecialty> GetUserSpecialties()
        {
            foreach (var item in _context.UsersSpecialties)
            {
                yield return item;
            }
        }

        public UserSpecialty GetUserSpecialtyById(int userSpecialtyId)
        {
            if (userSpecialtyId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.UsersSpecialties.FirstOrDefault(p => p.Id == userSpecialtyId);
        }

        public int Create(UserSpecialty userSpecialty)
        {
            if (userSpecialty is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.UsersSpecialties.Add(userSpecialty);
            _context.SaveChanges();
            return userSpecialty.Id;
        }
    }
}