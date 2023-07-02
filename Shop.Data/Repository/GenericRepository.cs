using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Common;

namespace Shop.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        #region constructor

        private readonly ShopContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ShopContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #endregion

        public async Task<int> AddAsync(T entity)
        {
            entity.CreateDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetEntityAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetEntityByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> IsEixstEntityByIdAsync(int id)
        {
            return await _dbSet.AnyAsync(t=> t.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
