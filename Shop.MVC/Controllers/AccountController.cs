using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.ViewModels.Account;

namespace Shop.MVC.Controllers
{
    public class AccountController : Controller
    {
        #region constructor

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        #region register

        [HttpGet("register")]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return NotFound();
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel register)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.AddUserAsync(register);

                switch (result)
                {
                    case RegisterUserResult.Success:
                        return View("SuccessRegister",new SuccessRegisterViewModel { FullName = register.FullName, Email = register.Email });
                    case RegisterUserResult.ExistUser:
                        ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت نام کرده است");
                        return View(register);
                }
            }

            return View(register);
        }


        #endregion

        #region Login

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            //if (!ModelState.ValidationState)
            //{
            //    return View(login);
            //}
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginAsync(login);
                switch (result)
                {
                    case LoginViewModel.LoginResult.Success:
                        return View(login);
                    case LoginViewModel.LoginResult.ExistUser:
                        return View(login);
                    default:
                        break;

                }
            }
            return View(login);
        }

        #endregion

        #region active account

        [HttpGet("active-account/{activeCode}")]
        public async Task<IActionResult> ActiveAccount(string activeCode)
        {
            bool ResultActiveAccount = await _accountService.ActiveAccountByEmailActiveCodeAsync(activeCode);
            TempData["resultActiveAccount"] = ResultActiveAccount;
            return View();
        }

        #endregion
    }
}
