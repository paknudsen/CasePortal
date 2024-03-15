using NK.Data.Repositories.Entities;
using NK.DataRepositories;
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
            using (var repo = new ProductRepository())
            {
                var products = repo.Products.Get().Select(t => new ProductViewModel(t.ProductId.ToString())
                {
                    Name = t.Name,
                    Description = t.ShortDescription,
                    SalesPrice = (decimal)t.FinalSalesPrice,
                    ImageUrl = t.ImageLargeUrl,
                }).ToList();

                return products;
            }
        }
    }
}
