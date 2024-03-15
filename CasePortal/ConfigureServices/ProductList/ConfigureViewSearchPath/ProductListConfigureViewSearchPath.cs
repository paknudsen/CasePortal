using NK.BusinessProcess.Web.ConfigureViewSearchPathCore;

namespace NK.Web.CasePortal.ConfigureServices.Home.ConfigureViewSearchPath
{
    public class ProductListConfigureViewSearchPath : ConfigureViewSearchPathBase
    {
        protected override List<string> ViewLocationPaths => new List<string>()
        {
            "/Controls/ProductList/Views/"
        };
    }
}
