using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Application.ViewModels.Account;

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
                        TempData["IsSuccessRegister"] = true;
                        return View(register);
                    case RegisterUserResult.ExistUser:
                        ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت نام کرده است");
                        return View(register);
                    default:
                        break;
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
    }
}
