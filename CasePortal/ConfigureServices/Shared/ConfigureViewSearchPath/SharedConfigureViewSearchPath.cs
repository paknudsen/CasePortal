using NK.BusinessProcess.Web.ConfigureViewSearchPathCore;

namespace CasePortal.ConfigureServices.Administration.ConfigureViewSearchPath
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
