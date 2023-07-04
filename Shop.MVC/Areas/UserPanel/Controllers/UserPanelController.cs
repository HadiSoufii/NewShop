using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.UserPanel.Controllers
{
    public class UserPanelController : UserBaseController
    {
        #region index

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            return View();
        }

        #endregion
    }
}
