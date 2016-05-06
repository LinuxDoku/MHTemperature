using Owin;

namespace MHTemperature.Service.Web {
    /// <summary>
    /// Owin Startup class.
    /// </summary>
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.UseNancy();
        }
    }
}