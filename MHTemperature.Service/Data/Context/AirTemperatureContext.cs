using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MHTemperature.Service.Data.Model;
using MHTemperature.Service.Extensions;

namespace MHTemperature.Service.Data.Context {
    public class AirTemperatureContext : ContextBase<AirTemperature> {
        protected override DbSet<AirTemperature> DbSet => Db.AirTemperatures;

        public AirTemperature GetLastAirTemperature() {
            return DbSet.OrderByDescending(x => x.MeasuredAt).FirstOrDefault();
        }

        public IEnumerable<AirTemperature> GetAirTemperatures() {
            return DbSet.OrderBy(x => x.MeasuredAt);
        }

        public IEnumerable<AirTemperature> GetAirTemperaturesMeasuredBetween(DateTime from, DateTime to) {
            return DbSet.Between(from, to).OrderBy(x => x.MeasuredAt);
        }
    }
}