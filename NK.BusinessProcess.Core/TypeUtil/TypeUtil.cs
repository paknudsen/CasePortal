using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NK.Utils.TypeUtil
{
    public static class TypeLoaderExtensions
    {
        public static IEnumerable<Type> GetLoadableTypes(this System.Reflection.Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            try
            {
                return assembly.GetTypes().Where(x=> x.IsClass && !x.IsAbstract);
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
