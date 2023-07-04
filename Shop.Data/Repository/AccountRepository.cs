using Microsoft.EntityFrameworkCore;
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

        #endregion

        public async Task<int> AddAsync(User user)
        {
            user.CreateDate = DateTime.Now;
            await _context.AddAsync(user);
            await SaveChangesAsync();
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailActiveCodeAsync(string emailActiveCode)
        {
            return await _context.Users.FirstOrDefaultAsync(u=> u.EmailActiveCode == emailActiveCode );
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserExistByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.AnyAsync(c=>c.Email== email && c.Password == password);
        }
    }
}
