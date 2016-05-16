using Nancy;
using Newtonsoft.Json;

namespace MHTemperature.Service.Web {
    public class ApiModuleBase : NancyModule {
        public ApiModuleBase() {}
        public ApiModuleBase(string modulePath) : base(modulePath) {}

        protected Response Json(object obj) {
            var response = (Response)JsonConvert.SerializeObject(obj);
            response.ContentType = "application/json";
            return response;
        }
    }
}