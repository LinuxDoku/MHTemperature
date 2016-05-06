using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Infrastructure {
    public class ServiceHost : IService {
        private readonly IService[] _services;

        public ServiceHost(params IService[] services) {
            _services = services;
        }

        public void Start() {
            foreach (var service in _services) {
                service.Start();
            }
        }

        public void Stop() {
            foreach (var service in _services) {
                service.Stop();
            }
        }
    }
}