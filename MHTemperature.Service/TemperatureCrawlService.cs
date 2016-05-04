using System;
using MHTemperature.Contracts;
using MHTemperature.Service.Data;
using MHTemperature.Service.Data.Model;
using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service {
    public class TemperatureCrawlService : ScheduledServiceBase {
        protected override TimeSpan Interval => TimeSpan.FromMinutes(15);

        protected override void Execute() {
            ITemperature current = null;
            Temperature temperature = null;

            try {
                var service = new TemperatureService();
                current = service.Current();
            }
            catch (Exception ex) {
                Logger.Error("Could not receive current temperature from webservice!", ex);
            }

            try {
                temperature = Temperature.CreateFrom(current);
                SaveToDatabase(temperature);
            }
            catch (Exception ex) {
                Logger.Error($"Could not save temperature to database! {temperature}", ex);
            }
        }

        private void SaveToDatabase(Temperature temperature) {
            var db = new Database();
            db.Temperatures.Add(temperature);
            db.SaveChanges();
        }
    }
}