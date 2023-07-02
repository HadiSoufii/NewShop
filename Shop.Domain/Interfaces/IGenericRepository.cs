using Shop.Domain.Models.Common;

namespace Shop.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetEntityAllAsync();
        Task<T?> GetEntityByIdAsync(int id);
        Task<bool> IsEixstEntityByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
    }
}
