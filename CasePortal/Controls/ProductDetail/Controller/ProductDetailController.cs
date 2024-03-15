using Microsoft.AspNetCore.Mvc;
using NK.Web.CasePortal.Controls.ProductDetail.Models;
using System.Net;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    
    public class ProductDetailController : Controller
    {

        [Route("ProductDetail/{id?}")]
        public IActionResult Index(string? id)
        {
            if (id == null) return NotFound();

            return View("ProductDetail",new ProductDetailViewModel("TestProduct"));
        }
    }
}
