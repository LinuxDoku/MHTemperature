using System;
using System.Threading;
namespace MHTemperature.Service {
    public class TemperatureCrawlService : ScheduledServiceBase {
        protected override TimeSpan Interval => TimeSpan.FromMinutes(15);

        protected override void Execute() {
            var service = new TemperatureService();
            var current = service.Current();


        }
    }
}