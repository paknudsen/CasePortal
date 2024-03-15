using NK.Data.Repositories.Entities;
using NK.DataRepositories;
using NK.Web.CasePortal.Controls.Base.Models;
using NK.Web.CasePortal.Controls.ProductDetail.Models;

namespace NK.Web.CasePortal.Controls.ProductDetail
{
    public interface IProductDetailModelFactoryData
    {
        Product GetProduct(int productId);
    }

    public class ProductDetailModelFactoryData : IProductDetailModelFactoryData
    {

        public Product GetProduct(int productId)
        {
            using (var repo = new ProductRepository())
            {
                return repo.Products.FirstOrDefault(p => p.ProductId == productId);
            }
        }
    }
}
