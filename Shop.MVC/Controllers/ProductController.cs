using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.ViewModels.Product;

namespace Shop.MVC.Controllers
{
    public class ProductController : SiteBaseController
    {

        #region constructor

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region list product

        [HttpGet("product-list")]
        [HttpGet("products-list/{CategoryId}")]
        public async Task<IActionResult> Index(FilterProductViewModel filter)
        {
            ViewData["Categories"] = await _productService.GetAllCategories();

            filter = await _productService.FilterProducts(filter);

            if (filter.PageId > filter.GetLastPage() && filter.GetLastPage() != 0) return NotFound();
            return View(filter);
        }

        #endregion

        #region detail product

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var productDetail = await _productService.GetProductForShowDetailProductById(productId);
            if (productDetail == null) return NotFound();
            return View(productDetail);
        }

        #endregion

        #region get product json

        [HttpGet("product-autocomplete")]
        public async Task<IActionResult> GetSellerProductsJson(string title)
        {
            var data = await _productService.FilterProducstByTitle(title);
            return new JsonResult(data);
        }

        #endregion
    }
}
