using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.MVC.Areas.UserPanel.Controllers
{
    //[Authorize]
    [Area("UserPanel")]
    [Route("user-panel")]
    public class UserBaseController : Controller
    {

    }
}
