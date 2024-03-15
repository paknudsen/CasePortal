using Microsoft.AspNetCore.Mvc;
using NK.Web.CasePortal.Controls.ProductDetail.Models;
using System.Net;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    [Route("ProductDetail")]
    public class ProductDetailController : Controller
    {

        [Route("{productid?}")]
        public IActionResult ProductDetail(string? id)
        {
            if (id == null) return NotFound();

            return View(new ProductDetailViewModel("TestProduct"));
        }
    }
}
