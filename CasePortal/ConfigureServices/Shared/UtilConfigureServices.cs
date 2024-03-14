using CasePortal.ConfigureServices;
using NK.Utils.DateTimeUtil;

namespace CasePortal.ConfigureServices.Shared
{
    public class UtilConfigureServices : IConfigureServices
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
