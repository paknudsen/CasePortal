using Microsoft.AspNetCore.Mvc;
using NK.Web.CasePortal.Controls.ProductList;
using System.Diagnostics;

namespace NK.Web.CasePortal.Controls.Home.Controllers
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
        public IActionResult Index()
        {
            var viewModel = new ProductListViewModelFactory().CreateFrom();
            return (IActionResult)viewModel;
        }
    }
}
