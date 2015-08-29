using System.ComponentModel;
using Residata.Platform.Contract.Server.Worker;

namespace MHTemperature.Service {
    [RunInstaller(true)]
    public class Installer : Residata.Platform.Server.Worker.InstallerBase {
        public Installer() : base(InstallerType.Service) {}
    }
}