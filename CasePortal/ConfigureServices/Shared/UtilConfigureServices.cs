using NK.Utils.DateTimeUtil;

namespace NK.Web.CasePortal.ConfigureServices.Shared
{
    public class UtilConfigureServices : IConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
