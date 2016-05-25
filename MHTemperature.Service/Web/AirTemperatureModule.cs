using System;
using MHTemperature.Service.Data.Context;

namespace MHTemperature.Service.Web {
    public class AirTemperatureModule : ApiModuleBase {
        /// <summary>
        /// Database Context Factory.
        /// </summary>
        private AirTemperatureContext Db => new AirTemperatureContext();

        public AirTemperatureModule() : base("/weather") {
            Get["/airtemperature"] = x => Json(AirTemperature());
            Get["/airtemperatures"] = x => Json(AirTemperatures());
            Get["/airtemperature/today"] = x => Json(AirTemperatures(DateTime.Today, DateTime.Today.AddDays(1)));
            Get["/airtemperature/yesterday"] = x => Json(AirTemperatures(DateTime.Today.AddDays(-1), DateTime.Today));
            Get["/airtemperature/{from:datetime}/{to:datetime}"] = x => Json(AirTemperatures((DateTime)x.from, (DateTime)x.to));
        }

        public object AirTemperature() {
            return Db.GetLastAirTemperature();
        }

        public object AirTemperatures() {
            return Db.GetAirTemperatures();
        }

        public object AirTemperatures(DateTime from, DateTime to) {
            return Db.GetAirTemperaturesMeasuredBetween(from, to);
        }
    }
}