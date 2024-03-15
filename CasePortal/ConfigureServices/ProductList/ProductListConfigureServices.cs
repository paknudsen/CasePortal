using NK.Web.CasePortal.Controls.ProductList;

namespace NK.Web.CasePortal.ConfigureServices.ProductList
{
    public class ProductListConfigureServices : IConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductListViewModelFactoryData, ProductListViewModelFactoryData>();
            services.AddScoped<IProductListViewModelFactory, ProductListViewModelFactory>();
            
        }
    }
}
