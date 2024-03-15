using Microsoft.AspNetCore.Mvc;
using NK.Web.CasePortal.Controls.ProductDetail.Models;
using System.Net;
using System.Runtime.CompilerServices;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailModelFactory _productDetailModelFactory;
        public ProductDetailController(IProductDetailModelFactory productDetailModelFactory)
        {
            _productDetailModelFactory = productDetailModelFactory;
        }

        [Route("ProductDetail/{id?}")]
        public IActionResult Index(string? id)
        {
            if(!int.TryParse(id, out var productId)) return NotFound();

            return View("ProductDetail", _productDetailModelFactory.CreateFrom(int.Parse(id)));
        }
    }
}
