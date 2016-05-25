using System;
using MHTemperature.Contracts;
using MHTemperature.Service.Data.Context;
using MHTemperature.Service.Data.Model;
using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service.Service {
    /// <summary>
    /// Service to receive the temperature from the freibad webservice and store to database.
    /// </summary>
    public class TemperatureCrawlService : ScheduledServiceBase {
        private TemperatureContext CreateContext() {
            return new TemperatureContext();
        }

        protected override string Name => nameof(TemperatureCrawlService);
        protected override TimeSpan MinDelay => TimeSpan.FromMinutes(5);

        protected override TimeSpan PlanNextExecution() {
            var lastTemperature = CreateContext().GetLastTemperature();
            var next = RetrievalPlanner.Next(lastTemperature, DateTime.Now);

            // min delay protection
            if (DateTime.Now.Add(next) < LastExecution.Add(MinDelay)) {
                return MinDelay;
            }

            return next;
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
                SaveWhenNotAlreadyExists(temperature);
            }
            catch (Exception ex) {
                Logger.Error($"Could not save temperature to database! {temperature}", ex);
            }
        }

        /// <summary>
        /// Save the measured temperature to database when a temperature with this timestamp does not exists yet.
        /// </summary>
        /// <param name="temperature"></param>
        private void SaveWhenNotAlreadyExists(Temperature temperature) {
            var context = CreateContext();
            var lastTemperature = context.GetLastTemperature();

            if (lastTemperature == null || lastTemperature.MeasuredAt != temperature.MeasuredAt) {
                context.Save(temperature);
            }
        }
    }
}