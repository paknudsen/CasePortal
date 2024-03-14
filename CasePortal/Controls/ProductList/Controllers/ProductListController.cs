using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CasePortal.Controls.Home.Controllers
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
            return View();
        }
    }
}
