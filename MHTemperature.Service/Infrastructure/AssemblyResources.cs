using System.IO;
using System.Reflection;
using MHTemperature.Service.Data;

namespace MHTemperature.Service.Infrastructure {
    public static class AssemblyResources {
        /// <summary>
        /// Get resource as string from the currently executed assembly.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static string GetResource(string resourceName) {
            var assembly = Assembly.GetExecutingAssembly();
            return GetResource(assembly, resourceName);
        }

        public static string GetResource(Assembly assembly, string resourceName) {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }
}