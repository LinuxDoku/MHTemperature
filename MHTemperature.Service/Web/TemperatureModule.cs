using System;
using MHTemperature.Service.Data.Context;

namespace MHTemperature.Service.Web {
    public class TemperatureModule : ApiModuleBase {
        /// <summary>
        /// Temperature Context Factory.
        /// </summary>
        private TemperatureContext Db => new TemperatureContext();

        public TemperatureModule() {
            Get["/temperature"] = x => Json(Temperature());
            Get["/temperature/{measuredAt:datetime}"] = x => Json(Temperature((DateTime)x.measuredAt));

            Get["/temperatures"] = x => Json(Temperatures());
            Get["/temperatures/today"] = x => Json(Temperatures(DateTime.Today, DateTime.Today.AddDays(1)));
            Get["/temperatures/yesterday"] = x => Json(Temperatures(DateTime.Today.AddDays(-1), DateTime.Today));
            Get["/temperatures/{from:datetime}/{to:datetime}"] = x => Json(Temperatures((DateTime)x.from, (DateTime)x.to));
        }

        public object Temperature() {
            return Db.GetLastTemperature();
        }

        public object Temperature(DateTime measuredAt) {
            return Db.GetTemperatureByMeasuredAt(measuredAt);
        }

        public object Temperatures() {
            return Db.GetAllTemperatures();
        }

        public object Temperatures(DateTime from, DateTime to) {
            return Db.GetTemperaturesMeasuredBetween(from, to);
        }
    }
}