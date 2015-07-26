using Residata.Platform.Server.Worker;
using Topshelf;

namespace MHTemperature.Service {
    public class Service : ServiceControl {
        public bool Start(HostControl hostControl) {
            Worker.Main(new string[0]);

            return true;
        }

        public bool Stop(HostControl hostControl) {
            return true;
        }
    }
}