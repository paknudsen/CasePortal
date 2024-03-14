using NK.Utils.TypeUtil;

namespace CasePortal.ConfigureServices
{
    public interface IConfigureServices
    {
        void ConfigureServices(IServiceCollection services);
    }

    public static class ConfigureServicesFactory
    {
        public static List<IConfigureServices> GetConfigureServicesHandlers()
        {
            var result = new List<IConfigureServices>();
            var it = typeof(IConfigureServices);
            foreach (var type in System.Reflection.Assembly.GetExecutingAssembly().GetLoadableTypes().Where(it.IsAssignableFrom).Where(x=>!x.IsInterface).Distinct().ToList())
            {
                result.Add((IConfigureServices)Activator.CreateInstance(type));
            }

            return result;
        }
    }
}
