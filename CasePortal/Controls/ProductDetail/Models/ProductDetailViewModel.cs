using NK.Web.CasePortal.Controls.Base.Models;

namespace NK.Web.CasePortal.Controls.ProductDetail.Models
{
    public class ProductDetailViewModel : ProductViewModel
    {
        public ProductDetailViewModel(string productId) : base(productId)
        { }

        public ProductDetailViewModel(ProductViewModel product) : base(product.ProductId)
        { 
        

        }

    }
}