using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/user")]
    public class AccountController : Controller
    {
        #region constructor

        #endregion


        #region list users

        [HttpGet("list-users")]
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
