using NK.Web.CasePortal.Controls.Base.Models;

namespace NK.Web.CasePortal.Controls.ProductDetail.Models
{
    public class ProductDetailViewModel
    {
        public ProductViewModel Product { get; set; }

        public ProductDetailViewModel(ProductViewModel product)
        { 
            Product = product;
        }
    }
}