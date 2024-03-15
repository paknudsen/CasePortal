using NK.BusinessProcess.Web.ConfigureViewSearchPathCore;

namespace NK.Web.CasePortal.ConfigureServices.Home.ConfigureViewSearchPath
{
    public class HomeConfigureViewSearchPath : ConfigureViewSearchPathBase
    {
        protected override List<string> ViewLocationPaths => new List<string>()
        {
            "/Controls/Home/Views/"
        };
    }
}
