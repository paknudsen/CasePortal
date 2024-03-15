using NK.BusinessProcess.Web.ConfigureViewSearchPathCore;

namespace NK.Web.CasePortal.ConfigureServices.Home.ConfigureViewSearchPath
{
    public class ProductDetailConfigureViewSearchPath : ConfigureViewSearchPathBase
    {
        protected override List<string> ViewLocationPaths => new List<string>()
        {
            "/Controls/ProductDetail/Views/"
        };
    }
}
