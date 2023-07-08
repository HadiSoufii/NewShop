using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.ViewModels.UserPanel;
using Shop.MVC.PresentationExtensions;

namespace Shop.MVC.ViewComponents
{
    public class CardProfileViewComponent : ViewComponent
    {
        #region constructor

        private readonly IAccountService _accountService;

        public CardProfileViewComponent(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserInformationViewModel? userInformation= null;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                userInformation =await _accountService.GetUserByIdForUserPanelAsync(userId);
            }
            return View("CardProfile",userInformation);
        }
    }
}
