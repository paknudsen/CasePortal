using Microsoft.AspNetCore.Mvc;
using NK.Web.CasePortal.Controls.ProductDetail.Models;
using System.Net;
using System.Runtime.CompilerServices;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    [Route("ProductDetail")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailModelFactory _productDetailModelFactory;
        public ProductDetailController(IProductDetailModelFactory productDetailModelFactory)
        {
            _productDetailModelFactory = productDetailModelFactory;
        }

        [Route("{productid?}")]
        public IActionResult ProductDetail(string? id)
        {
            if (id == null) return NotFound();

            if(!int.TryParse(id, out var productId)) return NotFound();

            return View(_productDetailModelFactory.CreateFrom(int.Parse(id)));
        }
    }
}
