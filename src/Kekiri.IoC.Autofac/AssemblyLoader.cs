using System;
using System.Reflection;
using System.IO;

namespace Kekiri.IoC.Autofac
{
    internal class AssemblyLoader
    {
        public static Assembly LoadFromFile(string path)
        {
#if NET452
            return Assembly.LoadFrom(path);
#else        
            return System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
#endif
        }
        
        public static string BaseDirectory
        {
            get
            {
#if NET452
                return AppDomain.CurrentDomain.BaseDirectory;
#else
                return AppContext.BaseDirectory;
#endif
            }
        }
    }
}