using System;
using MHTemperature.Service.Data.Context;
using MHTemperature.Service.Data.Model;
using NUnit.Framework;

namespace MHTemperature.Service.Tests.Data {
    /// <summary>
    /// Integration tests for the data context of AirTemperature model.
    /// </summary>
    [TestFixture]
    public class AirTemperatureContextTests : ContextTestBase<AirTemperatureContext, AirTemperature> {
        protected override AirTemperatureContext Context => new AirTemperatureContext();

        [Test]
        public void Should_Save_AirTemperature_To_Database() {
            var airTemperature = new AirTemperature {
                MeasuredAt = DateTime.Now,
                Temperature = 10.00m,
                RelativeHumidity = 1.00m,
                Station = new WeatherStation {
                    Name = "Test Sub Station"
                }
            };
            
            Context.Save(airTemperature);
        }

        [Test]
        public void Should_Not_Throw_Exception_On_GetLastTemperature() {
            Assert.DoesNotThrow(() => {
                Context.GetLastAirTemperature();
            });
        }
    }
}