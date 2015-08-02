using Residata.Platform.Contract.Application;
using Residata.Platform.Contract.Dependency.Attribute;
using Residata.Platform.Contract.Web;
using Residata.Platform.Core.Application;

namespace MHTemperature.Service.Web {
    [Export]
    public class WebApplication : ApplicationBase {
        public override string Name {
            get { return "MHTemperature.Service.Web"; }
        }

        public override bool Start(IApplicationBuilder builder) {
            builder.Use<IWebHost>(x => {
                x.Host = "127.0.0.1";
                x.Path = "/";
                x.Port = 82;
            });

            builder.Run();

            return true;
        }
    }
}