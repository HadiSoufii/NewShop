using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region index

        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
