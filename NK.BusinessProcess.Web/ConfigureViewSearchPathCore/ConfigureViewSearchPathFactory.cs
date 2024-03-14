using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NK.Utils.TypeUtil;

namespace NK.BusinessProcess.Web.ConfigureViewSearchPathCore
{

    public interface IConfigureViewSearchPathFactory
    {
        void SetViewSearchPath(IServiceCollection services);
    }

    public static class ConfigureViewSearchPathFactory
    {
        public static List<IConfigureViewSearchPathFactory> GetConfigureViewSearchPathHandlers(Assembly ass)
        {
            var result = new List<IConfigureViewSearchPathFactory>();
            var it = typeof(IConfigureViewSearchPathFactory);
            foreach (var type in ass.GetLoadableTypes().Where(it.IsAssignableFrom).Where(x => !x.IsInterface && !x.IsAbstract).Distinct().ToList())
            {
                result.Add((IConfigureViewSearchPathFactory)Activator.CreateInstance(type));
            }

            return result;
        }


    }
}
