using NK.DataRepositories;
using NK.Web.CasePortal.Controls.Base.Models;
using NK.Web.CasePortal.Controls.ProductDetail.Models;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    public interface IProductDetailModelFactoryData
    {
        ProductViewModel GetProduct(int productId);
    }

    public class ProductDetailModelFactoryData : IProductDetailModelFactoryData
    {
        public ProductViewModel GetProduct(int productId)
        {
            using (var repo = new ProductRepository())
            {
                var product = repo.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                {
                    return null;
                }

                return new ProductViewModel(productId.ToString())
                {
                    Name = product.Name,
                    Description = product.ShortDescription,
                    SalesPrice = (decimal)product.FinalSalesPrice,
                    ImageUrl = product.ImageLargeUrl

                };
                   

            }
        }
    }
}
