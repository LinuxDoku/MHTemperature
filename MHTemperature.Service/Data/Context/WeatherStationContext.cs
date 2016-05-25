using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MHTemperature.Service.Data.Model;

namespace MHTemperature.Service.Data.Context {
    public class WeatherStationContext : ContextBase<WeatherStation> {
        protected override DbSet<WeatherStation> DbSet => Db.WeatherStations;

        public IEnumerable<WeatherStation> GetWeatherStations() {
            return DbSet.OrderBy(x => x.StationId);
        }
    }
}