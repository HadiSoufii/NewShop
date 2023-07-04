using Shop.Application.Extensions;
using Shop.Application.Interfaces;
using Shop.Application.Senders;
using Shop.Application.Utils;
using Shop.Application.ViewModels.Account;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Account;

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

        public async Task<LoginViewModel.LoginResult> LoginAsync(LoginViewModel login)
        {
            if (await _accountRepository.IsUserExistByEmailAndPasswordAsync(login.Email,login.Password))
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
    }
}
