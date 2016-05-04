using System.Data.Entity;
using MHTemperature.Service.Data.Model;

namespace MHTemperature.Service.Data {
    public class Database : DbContext {
        public Database() : base("freibadmh") {}
        public DbSet<Temperature> Temperatures { get; set; }
    }
}