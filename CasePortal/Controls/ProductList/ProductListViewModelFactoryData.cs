using NK.Data.Repositories.Entities;
using NK.DataRepositories;
using NK.Web.CasePortal.Controls.Base.Models;

namespace NK.Web.CasePortal.Controls.ProductList
{
    public interface IProductListViewModelFactoryData
    {
        List<Product> GetAllProducts();
    }

    public class ProductListViewModelFactoryData : IProductListViewModelFactoryData
    {
        public List<Product> GetAllProducts()
        {
            using (var repo = new ProductRepository())
            {
                return repo.Products.Get().ToList();

            }
        }

    }
}
