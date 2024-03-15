using NK.Web.CasePortal.Controls.ProductList.Models;
using NK.Web.CasePortal.Controls.ProductList;
using NK.Web.CasePortal.Controls.ProductDetail.Models;
using NK.Web.CasePortal.NServiceBus;
using NK.Web.CasePortal.Controls.Base.Models;

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
            // Example for sending NSB message to service.
            //new BusSender().SendSimulateProductActionMessageMessage(productId);

            var product =  _productDetailModelFactoryData.GetProduct(productId);

            if (product == null) return null;

            var productViewModel = new ProductViewModel(productId.ToString())
            {
                Name = product.Name,
                Description = product.ShortDescription,
                SalesPrice = (decimal)product.FinalSalesPrice,
                ImageUrl = product.ImageLargeUrl

            };

            return new ProductDetailViewModel(productViewModel);
        }

    }
}
