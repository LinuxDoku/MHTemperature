using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DWD.Crawler.Contract.Model;

namespace MHTemperature.Service.Data.Model {
    [Table("weather_stations", Schema = "public")]
    public class WeatherStation : IStation {
        /// <summary>
        /// DWD ID of the Station. E.g. 3621 for Reimlingen.
        /// </summary>
        [Key, Column("id")]
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