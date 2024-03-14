using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace NK.BusinessProcess.Web.ConfigureViewSearchPathCore
{
    public abstract class ConfigureViewSearchPathBase : IConfigureViewSearchPathFactory
    {
        protected static bool IsCleared;
        protected abstract List<string> ViewLocationPaths { get; }

        public void SetViewSearchPath(IServiceCollection services)
        {
            if (ViewLocationPaths == null || !ViewLocationPaths.Any())
                return;

            foreach (string viewPath in ViewLocationPaths)
            {
                services.Configure<RazorViewEngineOptions>(o =>
                                                           {
                                                               if (IsCleared == false)
                                                               {
                                                                   // Should only be performed once
                                                                   o.ViewLocationFormats.Clear();
                                                                   IsCleared = true;
                                                               }

                                                               o.ViewLocationFormats.Add(viewPath + "{0}" + RazorViewEngine.ViewExtension);
                                                               if (viewPath.ToLower().EndsWith("views/"))
                                                               {
                                                                   o.ViewLocationFormats.Add(viewPath+ @"Components/" + "{0}" + RazorViewEngine.ViewExtension);
                                                                   o.ViewLocationFormats.Add(viewPath+ @"Components/Partials/" + "{0}" + RazorViewEngine.ViewExtension);
                                                                   o.ViewLocationFormats.Add(viewPath+ @"Partials/" + "{0}" + RazorViewEngine.ViewExtension);
                                                               }
                                                           });
            }
        }
    }
}
