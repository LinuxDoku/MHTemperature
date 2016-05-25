using MHTemperature.Service.Data.Context;

namespace MHTemperature.Service.Web {
    public class WeatherStationModule : ApiModuleBase {
        private WeatherStationContext Db = new WeatherStationContext();

        public WeatherStationModule() : base("/weather") {
            Get["/stations"] = x => Json(WeatherStations());
        }

        private object WeatherStations() {
            return Db.GetWeatherStations();
        }
    }
}