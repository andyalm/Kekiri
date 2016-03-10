using System.Reflection;
using System.Runtime.Loader;
using System.IO;

namespace Kekiri.IoC.Autofac
{
    internal class AssemblyLoader
    {
        public static Assembly LoadFromFile(string path)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
        }
    }
}