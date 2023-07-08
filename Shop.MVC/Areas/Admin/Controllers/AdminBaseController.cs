using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class AdminBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
