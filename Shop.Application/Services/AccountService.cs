using Shop.Application.Extensions;
using Shop.Application.Interfaces;
using Shop.Application.Senders;
using Shop.Application.Utils;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Account;
using Shop.Domain.ViewModels.Account;
using Shop.Domain.ViewModels.UserPanel;

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

            _sendEmailSerivce.SendActiveCodeByEmail(user.Email, user.FullName, user.EmailActiveCode, "_ActiveEmail", "فعالسازی حساب کاربری");

            return RegisterUserResult.Success;
        }

        public async Task<LoginViewModel.LoginResult> LoginAsync(LoginViewModel login)
        {
            if (await _accountRepository.IsUserExistByEmailAndPasswordAsync(login.Email, login.Password))
            {
                return LoginViewModel.LoginResult.ExistUser;
            }

            return LoginViewModel.LoginResult.Success;
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

        public async Task<bool> EditUserInUserPanelAsync(EditUserPanelViewModel editUser, int userId)
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
                        editUser.ImageName
                        );
                    user.ImageName = imageName;
                }

                await _accountRepository.UpdateAsync(user);

                return true;
            }

            return false;
        }

        public async Task<FilterUsersInAdminViewModel> GetUsersForAdminAsync(FilterUsersInAdminViewModel filter)
        {
            return await _accountRepository.FilterUsers(filter);
        }

        public async Task<CreateUserByAdminResult> AddUserByAdminAsync(CreateUserByAdminViewModel createUser)
        {
            if (await _accountRepository.IsUserExistByEmailAsync(createUser.Email))
                return CreateUserByAdminResult.ExistUser;            

            User user = new User
            {
                Email = createUser.Email.FixedEmail(),
                FullName = createUser.FullName,
                Password = createUser.Password.EncodePasswordMd5(),
                EmailActiveCode = Guid.NewGuid().ToString(),
                IsEmailActive = true,
                Mobile = createUser.Mobile,
                IsAdmin = createUser.IsAdmin
            };
            await _accountRepository.AddAsync(user);

            #region insert image

            if (createUser.Avatar != null && createUser.Avatar.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createUser.Avatar.FileName);
                createUser.Avatar.AddImageToServer(
                    imageName,
                    PathExtension.UserAvatarOriginServer,
                    100,
                    100,
                    PathExtension.UserAvatarThumbServer,
                    null
                    );
                user.ImageName = imageName;
            }

            #endregion

            return CreateUserByAdminResult.Success;
        }

        public async Task<UpdateUserByAdminViewModel?> GetUserForEditByAdminAsync(int id)
        {
            User? user = await _accountRepository.GetUserByIdAsync(id);
            if (user == null) return null;
            return new UpdateUserByAdminViewModel
            {
                Email = user.Email,
                ImageName = user.ImageName,
                FullName = user.FullName,
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                IsBan = user.IsBan,
                IsEmailActive = user.IsEmailActive,
                Mobile = user.Mobile,
            };
        }
        
        public async Task<bool> UpdateUserByAdminAsync(UpdateUserByAdminViewModel updateUser)
        {
            User? user = await _accountRepository.GetUserByIdAsync(updateUser.Id);
            if (user == null) return false;

            user.IsAdmin = updateUser.IsAdmin;
            user.IsBan = updateUser.IsBan;
            user.FullName = updateUser.FullName;
            user.Mobile = updateUser.Mobile;
            user.IsEmailActive = updateUser.IsEmailActive;

            if(!string.IsNullOrEmpty(updateUser.Password))
                user.Password = updateUser.Password.EncodePasswordMd5();

            #region insert image


            if (updateUser.Avatar != null && updateUser.Avatar.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(updateUser.Avatar.FileName);
                updateUser.Avatar.AddImageToServer(
                    imageName,
                    PathExtension.UserAvatarOriginServer,
                    100,
                    100,
                    PathExtension.UserAvatarThumbServer,
                    updateUser.ImageName
                    );
                user.ImageName = imageName;
            }

            #endregion

            return true;
        }
    }
}
