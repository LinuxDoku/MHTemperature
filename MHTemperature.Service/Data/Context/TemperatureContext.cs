using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MHTemperature.Service.Data.Model;

namespace MHTemperature.Service.Data.Context {
    public class TemperatureContext : ContextBase<Temperature> {
        protected override DbSet<Temperature> DbSet => Db.Temperatures;

        public Temperature GetLastTemperature() {
            return Db.Temperatures
                     .OrderByDescending(x => x.MeasuredAt)
                     .FirstOrDefault();
        }

        public Temperature GetTemperatureByMeasuredAt(DateTime measuredAt) {
            return Db.Temperatures
                     .FirstOrDefault(x => x.MeasuredAt == measuredAt);
        }

        public IEnumerable<Temperature> GetAllTemperatures() {
            return Db.Temperatures
                     .OrderBy(x => x.MeasuredAt);
        }

        public IEnumerable<Temperature> GetTemperaturesMeasuredInRange(DateTime from, DateTime to) {
            return Db.Temperatures
                     .Where(x => x.MeasuredAt >= from && x.MeasuredAt <= to)
                     .OrderBy(x => x.MeasuredAt);
        }
    }
}