using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.ViewModels.UserPanel;
using Shop.MVC.PresentationExtensions;

namespace Shop.MVC.Areas.UserPanel.ViewComponents
{
    public class UserSidebarViewComponent : ViewComponent
    {
        #region constructor

        private readonly IAccountService _accountService;

        public UserSidebarViewComponent(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int id = User.GetUserId();
            UserInformationViewModel userInformationModel = await _accountService.GetUserByIdForUserPanelAsync(id);
            return View("UserSidebar", userInformationModel);
        }
    }
}
