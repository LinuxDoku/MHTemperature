using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace MHTemperature.Service {
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer {
        public override void Install(IDictionary stateSaver) {
            var assembly = Context.Parameters["assemblypath"];
            stateSaver.Add("MHTemperature.Service.exe", assembly);
            ExecuteMainAssebly(assembly, "install start");

            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState) {
            ExecuteMainAssebly(Context.Parameters["assemblypath"], "uninstall");

            base.Uninstall(savedState);
        }

        private void ExecuteMainAssebly(string executable, string args) {
            var processStart = new ProcessStartInfo(executable, args);
            processStart.WindowStyle = ProcessWindowStyle.Hidden;

            using (var process = Process.Start(processStart)) {
                process.WaitForExit();
            }
        }
    }
}