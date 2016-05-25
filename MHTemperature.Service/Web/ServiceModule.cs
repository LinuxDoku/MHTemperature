using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service.Web {
    public class ServiceModule : ApiModuleBase {
        public ServiceModule() : base("/service") {
            Get["status"] = param => Json(Status());
        }

        public object Status() {
            return ServiceStatus.Instance.Status;
        }
    }
}