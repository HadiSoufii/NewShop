using Shop.Application.Interfaces;
using Shop.Application.Utils;
using Shop.Application.ViewModels.AccountVM;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Account;

namespace Shop.Application.Services
{
    public class AccountService : IAccountService
    {
        #region constructor

        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        #endregion

        public async Task<RegisterUserResult> AddUserAsync(RegisterUserViewModel register)
        {
            if (await _accountRepository.IsUserExistByEmailAsync(register.Email))
                return RegisterUserResult.ExistUser;

            User user = new User
            {
                Email = register.Email,
                FullName = register.FullName,
                Password = register.Password,
                ImageName = "Defult.jpg"

            };
            await _accountRepository.AddAsync(user);

            return RegisterUserResult.Success;
        }
    }
}
