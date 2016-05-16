using System.Collections.Generic;

namespace MHTemperature.Service.Infrastructure {
    public class ServiceStatus {
        private static ServiceStatus _instance;

        public static ServiceStatus Instance {
            get {
                if (_instance == null) {
                    _instance = new ServiceStatus();
                }

                return _instance;
            }
        }

        public Dictionary<string, object> Status = new Dictionary<string, object>();
    }
}