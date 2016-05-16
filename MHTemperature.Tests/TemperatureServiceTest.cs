using System;
using NUnit.Framework;
using MHTemperature.Contracts;

namespace MHTemperature.Tests {
    [TestFixture]
    public class TemperatureServiceTests {
        /// <summary>
        /// NOTE: Assert that you are connected to the internet when running this test.
        /// </summary>
        [Test]
        public void Should_Get_Current_Temperature() {
            var service = new TemperatureService();
            var temperature = service.Current();

            Assert.IsInstanceOf(typeof(ITemperatureService), service);
            Assert.NotNull(temperature);
            Assert.Greater(temperature.Swimmer, 0);
            Assert.Greater(temperature.NonSwimmer, 0);
            Assert.Greater(temperature.KidSplash, 0);
            Assert.NotNull(temperature.MeasuredAt);
        }

        [Test]
        public void Should_Parse_DateTime_Correctly() {
            var service = new TemperatureService();

            Assert.AreEqual(new DateTime(2015, 5, 5, 9, 4, 0), service.ParseDateTime("5.5.2015 9:4"));
            Assert.AreEqual(new DateTime(2015, 5, 5, 9, 40, 0), service.ParseDateTime("5.05.2015 9:40"));
            Assert.AreEqual(new DateTime(2015, 5, 5, 9, 4, 0), service.ParseDateTime("05.05.2015 09:04"));
            Assert.AreEqual(new DateTime(2015, 5, 10, 17, 4, 0), service.ParseDateTime("10.05.2015 17:04"));
            Assert.AreEqual(new DateTime(2015, 7, 8, 14, 57, 0), service.ParseDateTime("8.7.2015,  14:57 Uhr"));
        }
    }
}