using Residata.Platform.Server.Worker;
using Topshelf;

namespace MHTemperature.Service.Web {
    public class Service : ServiceControl {
        public bool Start(HostControl hostControl) {
            AppHostFactory.CreateAndRun<WebApplication>();

            return true;
        }

        public bool Stop(HostControl hostControl) {
            return true;
        }
    }
}