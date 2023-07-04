using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {

        [HttpGet("admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
