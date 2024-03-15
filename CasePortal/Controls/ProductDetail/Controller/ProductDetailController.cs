using Microsoft.AspNetCore.Mvc;
using NK.Web.CasePortal.Controls.ProductDetail.Models;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    [Route("ProductDetail")]
    public class ProductDetailController : Controller
    {

        [Route("{id?}")]
        public IActionResult Index(int? id)
        {
            return View(new ProductDetailViewModel("TestProduct"));
        }
    }
}
