﻿using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class AccountService : IAccountService
    {
        #region Fields

        protected readonly IRepository<User> _userRepository;

        #endregion

        #region Ctor

        public AccountService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Methods

        public virtual async Task InsertUserAsync(User user)
        {
            await _userRepository.InsertAsync(user);
        }

        public virtual async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.Table.FirstOrDefaultAsync(u => u.Name == username);
        }

        #endregion
    }
}