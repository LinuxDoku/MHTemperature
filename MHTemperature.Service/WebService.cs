using System.Configuration;
using System.Threading.Tasks;
using MHTemperature.Service.Contract;
using MHTemperature.Service.Web;
using Microsoft.Owin.Hosting;

namespace MHTemperature.Service {
    public class WebService : IService {
        public void Start() {
            var url = ConfigurationManager.AppSettings["webServiceUrl"];

            Task.Run(() => {
                WebApp.Start<Startup>(url);
            });
        }

        public void Stop() {
            
        }
    }
}