using Shop.Domain.ViewModels.Account;
using Shop.Domain.ViewModels.UserPanel;

namespace Shop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterUserResult> AddUserAsync(RegisterUserViewModel register);

        Task<LoginViewModel.LoginResult> LoginAsync(LoginViewModel login);
        Task<bool> ActiveAccountByEmailActiveCodeAsync(string emailActiveCode);
        Task<UserInformationViewModel> GetUserByIdForUserPanelAsync(int id);
        Task<EditUserPanelViewModel> GetUserByIdForEditUserPanelAsync(int id);
        Task<bool> EditUserInUserPanelAsync(EditUserPanelViewModel editUser, int userId);
        Task<FilterUsersInAdminViewModel> GetUsersForAdminAsync(FilterUsersInAdminViewModel filter);
        Task<CreateUserByAdminResult> AddUserByAdminAsync(CreateUserByAdminViewModel createUser);
        Task<UpdateUserByAdminViewModel?> GetUserForEditByAdminAsync(int id);
        Task<bool> UpdateUserByAdminAsync(UpdateUserByAdminViewModel updateUser);
    }
}
