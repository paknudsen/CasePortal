using NK.Web.CasePortal.Controls.ProductDetail;
using NK.Web.CasePortal.Controls.ProductList;

namespace NK.Web.CasePortal.ConfigureServices.ProductDetail
{

    public class ProductDetailConfigureServices : IConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductDetailModelFactoryData, ProductDetailModelFactoryData>();
            services.AddScoped<IProductDetailModelFactory, ProductDetailModelFactory>();
            

        }
    }
}
