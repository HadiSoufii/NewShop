﻿using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Application.ViewModels.UserPanel;
using Shop.MVC.PresentationExtensions;

namespace Shop.MVC.Areas.UserPanel.Controllers
{
    public class UserPanelController : UserBaseController
    {
        #region constructor

        private readonly IAccountService _accountService;

        public UserPanelController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        #region index

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        #endregion

        #region edit user

        [HttpGet("edit-user")]
        public async Task<IActionResult> EditUser()
        {
            int id = User.GetUserId();
            EditUserPanelViewModel model = await _accountService.GetUserByIdForEditUserPanelAsync(id);
            return View(model);
        }

        [HttpPost("edit-user"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserPanelViewModel editUserPanel)
        {
            if (ModelState.IsValid)
            {
                bool editResult = await _accountService.EditUserInUserPanel(editUserPanel, User.GetUserId());
                if (editResult)
                    TempData["SuccessEdit"] = true;
                else
                    TempData["SuccessEdit"] = false;

            }
            return View(editUserPanel);
        }

        #endregion
    }
}
