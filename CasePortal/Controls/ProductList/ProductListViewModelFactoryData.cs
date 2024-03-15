using NK.Data.Repositories.Entities;
using NK.Web.CasePortal.Controls.Base.Models;

namespace NK.Web.CasePortal.Controls.ProductList
{
    public interface IProductListViewModelFactoryData
    {
        List<ProductViewModel> GetAllProducts();
    }

    public class ProductListViewModelFactoryData : IProductListViewModelFactoryData
    {
        public List<ProductViewModel> GetAllProducts()
        {
            var list = new List<ProductViewModel>();

            for (int i = 0; i < 10; i++) {
                var product = new ProductViewModel($"PATEST {i}");
                product.SalesPrice = i;
                list.Add(product);  
            }
            return list;
        }
    }
}
