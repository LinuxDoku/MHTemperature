using MHTemperature.Service.Task;
using Residata.Platform.Contract.Application;
using Residata.Platform.Contract.Task;
using Residata.Platform.Core.Application;

namespace MHTemperature.Service {
    public class ServiceApplication : ApplicationBase {
        public override string Name {
            get { return "MHTemperature.Service"; }
        }

        public override bool Start(IApplicationBuilder builder) {
            builder.Use<ITaskProcessor>(x => {
                x.Schedule<CrawlTask>();
            });

            builder.Run();

            return true;
        }
    }
}