using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DWD.Crawler.Contract.Model;
using MHTemperature.Service.Contract;

namespace MHTemperature.Service.Data.Model {
    [Table("air_temperatures", Schema = "public")]
    public class AirTemperature : IAirTemperature, IMeasure {
        public AirTemperature() {}

        public AirTemperature(IAirTemperature airTempeature) {
            MeasuredAt = airTempeature.MeasuredAt;
            QualityLevel = airTempeature.QualityLevel;
            Temperature = airTempeature.Temperature;
            RelativeHumidity = airTempeature.RelativeHumidity;
            Station = new WeatherStation(airTempeature.Station);
        }

        [Key, Column("id")]
        public int AirTemperatureId { get; set; }
        
        [Column("station_id")]
        public int StationId { get; set; }

        /// <summary>
        /// Weather Station.
        /// </summary>
        public virtual WeatherStation Station { get; set; }

        /// <summary>
        /// DateTime when the temperature was measured.
        /// </summary>
        [Column("measured_at")]
        public DateTime MeasuredAt { get; set; }

        /// <summary>
        /// Quality level of the temperature.
        /// </summary>
        [Column("quality_level")]
        public IQualtityLevel QualityLevel { get; set; }

        /// <summary>
        /// Actual Temperature.
        /// </summary>
        [Column("temperature")]
        public decimal Temperature { get; set; }

        /// <summary>
        /// Relative humidity of the last 24 measures.
        /// </summary>
        [Column("relative_humidity")]
        public decimal RelativeHumidity { get; set; }

        /// <summary>
        /// Station where the temperature was measured.
        /// </summary>
        [NotMapped]
        IStation IAirTemperature.Station => Station;
    }
}