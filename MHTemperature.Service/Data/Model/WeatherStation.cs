using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DWD.Crawler.Contract.Model;

namespace MHTemperature.Service.Data.Model {
    [Table("weather_stations", Schema = "public")]
    public class WeatherStation : IStation {
        public WeatherStation() {}

        public WeatherStation(IStation station) {
            Name = station.Name;
            StationId = station.StationId;
            State = station.State;
            GeoLongitude = station.GeoLongitude;
            GeoLatitude = station.GeoLatitude;
        }

        /// <summary>
        /// Unique identifier in the database.
        /// </summary>
        [Key, Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// DWD ID of the Station. E.g. 3621 for Reimlingen.
        /// </summary>
        [Column("station_id")]
        public int StationId { get; set; }

        /// <summary>
        /// Name of the station.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// State in which the station is located. E.g. Bayern.
        /// </summary>
        [Column("state")]
        public string State { get; set; }

        /// <summary>
        /// Geo Longitude.
        /// </summary>
        [Column("geo_longitude")]
        public decimal GeoLongitude { get; set; }

        /// <summary>
        /// Geo Latitude.
        /// </summary>
        [Column("geo_latitude")]
        public decimal GeoLatitude { get; set; }
    }
}