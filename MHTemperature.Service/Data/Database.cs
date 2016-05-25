using System.Data.Entity;
using MHTemperature.Service.Data.Model;

namespace MHTemperature.Service.Data {
    public class Database : DbContext {
        public Database() : base("freibadmh") {
            DatabaseSchemaMigration.Migrate(this);
        }
        
        /// <summary>
        /// Water temperatures.
        /// </summary>
        public DbSet<Temperature> Temperatures { get; set; }

        /// <summary>
        /// Air temperatures.
        /// </summary>
        public DbSet<AirTemperature> AirTemperatures { get; set; }

        /// <summary>
        /// Weather stations.
        /// </summary>
        public DbSet<WeatherStation> WeatherStations { get; set; }
    }
}