using System.Data.Entity;
using MHTemperature.Service.Data.Model;

namespace MHTemperature.Service.Data.Context {
    public class AirTempeatureContext : ContextBase<AirTemperature> {
        protected override DbSet<AirTemperature> DbSet => Db.AirTemperatures;
    }
}