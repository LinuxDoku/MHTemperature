using System.Data.Entity;
using MHTemperature.Service.Data.Model;

namespace MHTemperature.Service.Data.Context {
    public class WeatherStationContext : ContextBase<WeatherStation> {
        protected override DbSet<WeatherStation> DbSet => Db.WeatherStations;
    }
}