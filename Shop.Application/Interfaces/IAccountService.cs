using Shop.Application.ViewModels.AccountVM;
using Shop.Domain.Models.Account;

namespace Shop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterUserResult> AddUserAsync(RegisterUserViewModel register);
    }
}
