using NK.BusinessProcess.Web.ConfigureViewSearchPathCore;

namespace NK.Web.CasePortal.ConfigureServices.Administration.ConfigureViewSearchPath
{
    public class SharedConfigureViewSearchPath : ConfigureViewSearchPathBase
    {

        protected override List<string> ViewLocationPaths => new List<string>()
        {
            "/Controls/Shared/Views/",
            "/",
        };

    }
}
