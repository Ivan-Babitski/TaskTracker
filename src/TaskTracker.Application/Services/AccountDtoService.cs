using System;
using System.Collections.Generic;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Mapper;
using TaskTracker.Application.Models;
using TaskTracker.Core.Interfaces;

namespace TaskTracker.Application.Services
{
    public class AccountDtoService : IAccountDtoService
    {
        private IFriendshipsRepository _friendshipsRepository;
        public AccountDtoService(IFriendshipsRepository friendshipsRepository)
        {
            _friendshipsRepository = friendshipsRepository;
        }
        public IEnumerable<UserDto> GetFriendsByUserId(string currentUserId)
        {
            try
            {
                var model = _friendshipsRepository.GetFriendsByUserId(currentUserId);
                return ObjectMapper.Mapper.Map<IEnumerable<UserDto>>(model);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
