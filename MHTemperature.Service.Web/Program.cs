using System.Diagnostics;
using Residata.Platform.Server.Worker;

namespace MHTemperature.Service.Web {
    public class Program : Worker {
        public static void Main(string[] args) {
            Worker.Main(args);
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
