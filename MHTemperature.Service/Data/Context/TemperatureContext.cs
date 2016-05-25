using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MHTemperature.Service.Data.Model;
using MHTemperature.Service.Extensions;

namespace MHTemperature.Service.Data.Context {
    public class TemperatureContext : ContextBase<Temperature> {
        protected override DbSet<Temperature> DbSet => Db.Temperatures;

        public Temperature GetLastTemperature() {
            return DbSet.OrderByDescending(x => x.MeasuredAt)
                        .FirstOrDefault();
        }

        public Temperature GetTemperatureByMeasuredAt(DateTime measuredAt) {
            return DbSet.FirstOrDefault(x => x.MeasuredAt == measuredAt);
        }

        public IEnumerable<Temperature> GetAllTemperatures() {
            return DbSet.OrderBy(x => x.MeasuredAt);
        }

        public IEnumerable<Temperature> GetTemperaturesMeasuredBetween(DateTime from, DateTime to) {
            return DbSet.Between(from, to).OrderBy(x => x.MeasuredAt);
        }
    }
}