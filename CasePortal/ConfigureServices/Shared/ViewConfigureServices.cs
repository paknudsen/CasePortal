using NK.BusinessProcess.Web.View;

namespace NK.Web.CasePortal.ConfigureServices.Shared
{
    public class ViewConfigureServices : IConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRenderViewHelper, RenderViewHelper>();
        }
    }
}
