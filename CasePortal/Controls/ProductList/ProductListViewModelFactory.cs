
using NK.Web.CasePortal.Controls.ProductList.Models;

namespace NK.Web.CasePortal.Controls.ProductList
{
    public interface IProuctListViewModelFactory
    {
        ProductListViewModel CreateFrom();
    }

    public class ProductListViewModelFactory
    {
        private readonly IProductListViewModelFactoryData _productListViewModelFactoryData;

        public ProductListViewModelFactory(IProductListViewModelFactoryData productListViewModelFactoryData)
        {
            _productListViewModelFactoryData = productListViewModelFactoryData;
        }

        public ProductListViewModelFactory():this(new ProductListViewModelFactoryData())
        {

        }

        public ProductListViewModel CreateFrom()
        {
            var viewModel = new ProductListViewModel();
            viewModel.Products = _productListViewModelFactoryData.GetAllProducts();

            return viewModel;
        }
    }
}
