using NK.Web.CasePortal.Controls.ProductList.Models;
using NK.Web.CasePortal.Controls.ProductList;
using NK.Web.CasePortal.Controls.ProductDetail.Models;

namespace NK.Web.CasePortal.Controls.ProductDetail
{

    public interface IProductDetailModelFactory
    {
        ProductDetailViewModel CreateFrom(int productId);
    }

    public class ProductDetailModelFactory : IProductDetailModelFactory
    {
        private readonly IProductDetailModelFactoryData _productDetailModelFactoryData;

        public ProductDetailModelFactory(IProductDetailModelFactoryData productDetailModelFactoryData)
        {
            _productDetailModelFactoryData = productDetailModelFactoryData;
        }

        public ProductDetailViewModel CreateFrom(int productId)
        {
            return new ProductDetailViewModel(_productDetailModelFactoryData.GetProduct(productId));

        }

    }
}
