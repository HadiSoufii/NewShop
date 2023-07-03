using Shop.Application.ViewModels.Account;
using Shop.Application.ViewModels.UserPanel;

namespace Shop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterUserResult> AddUserAsync(RegisterUserViewModel register);
        Task<bool> ActiveAccountByEmailActiveCodeAsync(string emailActiveCode);
        Task<UserInformationViewModel> GetUserByIdForUserPanelAsync(int id);
    }
}
