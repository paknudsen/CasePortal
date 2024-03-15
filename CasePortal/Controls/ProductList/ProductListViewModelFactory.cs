
using NK.Web.CasePortal.Controls.Base.Models;
using NK.Web.CasePortal.Controls.ProductList.Models;

namespace NK.Web.CasePortal.Controls.ProductList
{
    public interface IProductListViewModelFactory
    {
        ProductListViewModel CreateFrom();
    }

    public class ProductListViewModelFactory: IProductListViewModelFactory
    {
        private readonly IProductListViewModelFactoryData _productListViewModelFactoryData;

        public ProductListViewModelFactory(IProductListViewModelFactoryData productListViewModelFactoryData)
        {
            _productListViewModelFactoryData = productListViewModelFactoryData;
        }

        public ProductListViewModel CreateFrom()
        {
            var viewModel = new ProductListViewModel();
            var products = _productListViewModelFactoryData.GetAllProducts();

            viewModel.Products = products.Select(t => new ProductViewModel(t.ProductId.ToString())
            {
                Name = t.Name,
                Description = t.ShortDescription,
                SalesPrice = (decimal)t.FinalSalesPrice,
                ImageUrl = t.ImageLargeUrl,
            }).ToList(); ;

            return viewModel;
        }
    }
}
