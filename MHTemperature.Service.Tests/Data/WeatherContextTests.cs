using MHTemperature.Service.Data.Context;
using MHTemperature.Service.Data.Model;
using NUnit.Framework;

namespace MHTemperature.Service.Tests.Data {
    [TestFixture]
    public class WeatherStationContextTests : ContextTestBase<WeatherStationContext, WeatherStation> {
        protected override WeatherStationContext Context => new WeatherStationContext();

        [Test]
        public void Should_Save_WeatherStation_To_Database() {
            var weatherStation = new WeatherStation {
                Name = "Test",
                GeoLatitude = 10.00m,
                GeoLongitude = 4.00m,
                State = "Bayern"
            };

            Context.Save(weatherStation);
        }
    }
}