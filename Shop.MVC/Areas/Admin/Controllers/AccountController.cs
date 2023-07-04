using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.ViewModels.Account;

namespace Shop.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/user/")]
    public class AccountController : Controller
    {
        #region constructor

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion


        #region list users

        [HttpGet("list-users")]
        public async Task<IActionResult> Index(FilterUsersInAdminViewModel filter)
        {
            filter = await _accountService.GetUsersForAdminAsync(filter);
            return View(filter);
        }

        #endregion

        #region create user 

        [HttpGet("create-user")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost("create-user"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserByAdminViewModel createUser)
        {
            if (ModelState.IsValid)
            {
                var resultCreateUser = await _accountService.AddUserByAdminAsync(createUser);
                switch (resultCreateUser)
                {
                    case CreateUserByAdminResult.Success:
                        return RedirectToAction("Index");
                    case CreateUserByAdminResult.ExistUser:
                        ModelState.AddModelError("Email","ایمیل وارد شده قبلا ثبت نام کرده است");
                        break;
                }
            }
            return View(createUser);
        }

        #endregion

        #region edit user 

        [HttpGet("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id)
        {
            UpdateUserByAdminViewModel updateUser = await _accountService.GetUserForEditByAdminAsync(id);
            if (updateUser == null) return NotFound();
            return View(updateUser);
        }

        [HttpPost("update-user"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserByAdminViewModel updateUser)
        {
            if (ModelState.IsValid)
            {
                var resultUpdate = await _accountService.UpdateUserByAdminAsync(updateUser);
                if (resultUpdate)
                    return RedirectToAction("Index");
            }
            return View(updateUser);
        }

        #endregion
    }
}
