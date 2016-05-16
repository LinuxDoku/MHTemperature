using System;
using MHTemperature.Contracts;
using MHTemperature.Service.Data.Context;
using MHTemperature.Service.Data.Model;
using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service {
    public class TemperatureCrawlService : ScheduledServiceBase {
        private TemperatureContext CreateContext() {
            return new TemperatureContext();
        }

        protected override string Name => nameof(TemperatureCrawlService);

        protected override TimeSpan PlanNextExecution() {
            var lastTemperature = CreateContext().GetLastTemperature();
            return RetrievalPlanner.Next(lastTemperature, DateTime.Now);
        }

        protected override void Execute() {
            ITemperature current = null;
            Temperature temperature = null;

            try {
                var service = new TemperatureService();
                current = service.Current();
            }
            catch (Exception ex) {
                Logger.Error("Could not receive current temperature from webservice!", ex);
                return;
            }

            try {
                temperature = Temperature.CreateFrom(current);
                CreateContext().Save(temperature);
            }
            catch (Exception ex) {
                Logger.Error($"Could not save temperature to database! {temperature}", ex);
            }
        }
    }
}