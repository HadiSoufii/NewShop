﻿using Shop.Application.ViewModels.Account;

namespace Shop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterUserResult> AddUserAsync(RegisterUserViewModel register);
        Task<bool> ActiveAccountByEmailActiveCodeAsync(string emailActiveCode);
    }
}
