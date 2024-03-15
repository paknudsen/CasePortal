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
            throw new NotImplementedException();
        }
    }
}
