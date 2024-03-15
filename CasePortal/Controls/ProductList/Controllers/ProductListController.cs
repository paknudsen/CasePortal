using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace NK.Web.CasePortal.Controls.ProductList
{
    [Route("ProductList")]
    public class ProductListController : Controller
    {
        private readonly ILogger<ProductListController> _logger;
        private readonly IProductListViewModelFactory _productListViewModelFactory;

        public ProductListController(ILogger<ProductListController> logger, IProductListViewModelFactory productListViewModelFactory)
        {
            _logger = logger;
            _productListViewModelFactory = productListViewModelFactory;
        }

        [Route("")]
        public IActionResult ProductList()
        {
            var viewModel = _productListViewModelFactory.CreateFrom();
            return View(viewModel);
        }
    }
}
