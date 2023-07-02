﻿using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Account;

namespace Shop.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        #region constructor

        private readonly ShopContext _context;

        public AccountRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(User user)
        {
            user.CreateDate = DateTime.Now;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
               await DeleteAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsEixstUserByIdAsync(int id)
        {
            return await _context.Users.AnyAsync(u=> u.Id == id);
        }

        #endregion

        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
