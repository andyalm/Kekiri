using System.Reflection;
using System.IO;

namespace Kekiri.IoC.Autofac
{
    internal class AssemblyLoader
    {
        public static Assembly LoadFromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            
            return Assembly.Load(bytes);
        }
    }
}