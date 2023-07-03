using Shop.Application.Extensions;
using Shop.Application.Interfaces;
using Shop.Application.Senders;
using Shop.Application.Utils;
using Shop.Application.ViewModels.Account;
using Shop.Application.ViewModels.UserPanel;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shop.Application.Services
{
    public class AccountService : IAccountService
    {
        #region constructor

        private readonly IAccountRepository _accountRepository;
        private readonly ISendEmailSerivce _sendEmailSerivce;

        public AccountService(IAccountRepository accountRepository, ISendEmailSerivce sendEmailSerivce)
        {
            _accountRepository = accountRepository;
            _sendEmailSerivce = sendEmailSerivce;
        }

        #endregion

        public async Task<RegisterUserResult> AddUserAsync(RegisterUserViewModel register)
        {
            if (await _accountRepository.IsUserExistByEmailAsync(register.Email))
                return RegisterUserResult.ExistUser;

            User user = new User
            {
                Email = register.Email.FixedEmail(),
                FullName = register.FullName,
                Password = register.Password.EncodePasswordMd5(),
                EmailActiveCode = Guid.NewGuid().ToString(),

            };
            await _accountRepository.AddAsync(user);

           _sendEmailSerivce.SendActiveCodeByEmail(user.Email,user.FullName,user.EmailActiveCode, "_ActiveEmail", "فعالسازی حساب کاربری");

            return RegisterUserResult.Success;
        }

        public async Task<bool> ActiveAccountByEmailActiveCodeAsync(string emailActiveCode)
        {
            var user = await _accountRepository.GetUserByEmailActiveCodeAsync(emailActiveCode);
            if (user == null || user.IsEmailActive || user.EmailActiveCode != emailActiveCode) return false;
            user.IsEmailActive = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();
            await _accountRepository.UpdateAsync(user);
            return true;
        }

        public async Task<UserInformationViewModel> GetUserByIdForUserPanelAsync(int id)
        {
            var user = await _accountRepository.GetUserByIdAsync(id);
            return new UserInformationViewModel
            {
                FullName = user?.FullName ?? "",
                Mobile = user?.Mobile,
                ImageName = user?.ImageName,
                WalletBalance = 0,
                Score = 0
            };
        }

        public async Task<EditUserPanelViewModel> GetUserByIdForEditUserPanelAsync(int id)
        {
            var user = await _accountRepository.GetUserByIdAsync(id);
            return new EditUserPanelViewModel
            {
                FullName = user?.FullName ?? "",
                Mobile = user?.Mobile,
                ImageName = user?.ImageName,
            };
        }

        public async Task<bool> EditUserInUserPanel(EditUserPanelViewModel editUser, int userId)
        {
            var user = await _accountRepository.GetUserByIdAsync(userId);

            if (user != null)
            {
                user.FullName = editUser.FullName;
                user.Mobile = editUser.Mobile;

                if (editUser.Avatar != null && editUser.Avatar.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editUser.Avatar.FileName);
                    editUser.Avatar.AddImageToServer(
                        imageName,
                        PathExtension.UserAvatarOriginServer,
                        100,
                        100,
                        PathExtension.UserAvatarThumbServer,
                        imageName
                        );
                    user.ImageName = imageName;
                }

               await _accountRepository.UpdateAsync(user);

                return true;
            }

            return false;
        }
    }
}
