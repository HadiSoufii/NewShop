using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
