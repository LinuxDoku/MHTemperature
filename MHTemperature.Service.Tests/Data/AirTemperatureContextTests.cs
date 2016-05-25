﻿using System;
using MHTemperature.Service.Data.Context;
using MHTemperature.Service.Data.Model;
using NUnit.Framework;

namespace MHTemperature.Service.Tests.Data {
    /// <summary>
    /// Integration tests for the data context of AirTemperature model.
    /// </summary>
    [TestFixture]
    public class AirTemperatureContextTests : ContextTestBase<AirTempeatureContext, AirTemperature> {
        protected override AirTempeatureContext Context => new AirTempeatureContext();

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
    }
}