using Shop.Domain.Models.Account;

namespace Shop.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<bool> IsUserExistByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> IsEixstUserByIdAsync(int id);
        Task<int> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task DeleteAsync(User user);
    }
}
