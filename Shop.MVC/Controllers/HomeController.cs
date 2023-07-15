using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.Models.Common;

namespace Shop.MVC.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region constructor

        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;;
        }

        #endregion

        #region index

        public async Task<IActionResult> Index()
        {
            var productCards = await _productService.GetLastProductForShowHome();
            return View(productCards);
        }

        #endregion
    }
}
