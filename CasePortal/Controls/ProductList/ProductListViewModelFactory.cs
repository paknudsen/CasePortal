
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
            viewModel.Products = _productListViewModelFactoryData.GetAllProducts();

            return viewModel;
        }
    }
}
