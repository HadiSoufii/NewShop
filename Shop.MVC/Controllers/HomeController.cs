using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region index

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
