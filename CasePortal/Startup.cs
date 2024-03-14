using System.Globalization;
using System.Reflection;
using log4net;
using NK.Data.Repositories.Extensions;
using NK.DataRepositories;

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

    public static void PopulateProductMockData()
    {
        using (ProductRepository repo = new ProductRepository())
        {
            repo.Products.FillProductRepositoryMock();
            repo.Commit();
        }
    }
   
}