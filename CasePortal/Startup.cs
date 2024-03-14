using System.Globalization;
using System.Reflection;
using log4net;

public class Startup
{
    /// <summary>
    /// Sets the applications culture info to no-NO
    /// </summary>
    public static void SetDefaultApplicationCulture()
    {
        var cultureInfo = new CultureInfo("no-NO");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
   
}