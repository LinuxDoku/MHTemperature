using Residata.Platform.Contract.Application;
using Residata.Platform.Contract.Web;
using Residata.Platform.Core.Application;

namespace MHTemperature.Service.Web {
    public class WebApplication : ApplicationBase {
        public override string Name {
            get { return "MHTemperature.Service.Web"; }
        }

        public override bool Start(IApplicationBuilder builder) {
            builder.Use<IWebHost>(x => {
                x.Path = "/";
                x.Port = 8080;
            });

            builder.Run();

            return true;
        }
    }
}