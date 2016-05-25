using System;
using System.Linq;
using DWD.Crawler.Provider;
using MHTemperature.Service.Data.Context;
using MHTemperature.Service.Data.Model;
using MHTemperature.Service.Infrastructure;

namespace MHTemperature.Service.Service {
    public class WeatherCrawlService : ScheduledServiceBase {
        protected override string Name => nameof(WeatherCrawlService);
        protected override TimeSpan MinDelay => TimeSpan.FromMinutes(5);

        private T CreateContext<T>() where T : new() {
            return new T();
        }

        protected override void Execute() {
            LoadStationList();
            LoadAirTemperatures();
        }

        private void LoadStationList() {
            var context = CreateContext<WeatherStationContext>();

            var stationProvider = new StationProvider();
            var stations = stationProvider.GetAll();

            var stationsInDatabase = context.GetWeatherStations().Select(x => x.Name);
            var stationsNotInDatabase = stations.Where(station => !stationsInDatabase.Contains(station.Name));

            foreach (var station in stationsNotInDatabase) {
                context.Save(new WeatherStation(station));
            }
        }

        private void LoadAirTemperatures() {
            var context = CreateContext<AirTemperatureContext>();

            var airTemperatureProvider = new AirTemperatureProvider();
            
            // TODO: find nearest station
        }
    }
}