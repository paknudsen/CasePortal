using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace NK.Web.CasePortal.Controls.ProductList
{
    [Route("ProductList")]
    public class ProductListController : Controller
    {
        private readonly ILogger<ProductListController> _logger;

        public ProductListController(ILogger<ProductListController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        public IActionResult ProductList()
        {
            var viewModel = new ProductListViewModelFactory().CreateFrom();
            return View(viewModel);
        }
    }
}
