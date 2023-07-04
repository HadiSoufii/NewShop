using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.UserPanel.ViewComponents
{
    public class UserSidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("UserSidebar");
        }
    }
}
