using System;
using MHTemperature.Contracts;
using Moq;
using NUnit.Framework;

namespace MHTemperature.Tests {
    [TestFixture]
    public class RetrievalPlannerTests {
        [Test]
        public void Should_Plan_Correct_Retrieval_When_No_Temperature_Is_Passed() {
            var timeSpan = RetrievalPlanner.Next(null, DateTime.Now);

            Assert.AreEqual(TimeSpan.Zero, timeSpan);
        }

        [Test]
        public void Should_Plan_Correct_Retrieval_Before_6_00_AM() {
            var temperature = new Mock<ITemperature>().Object;
            var dateTime = new DateTime(2016, 5, 16, 5, 00, 0);

            var timeSpan = RetrievalPlanner.Next(temperature, dateTime);
           
            Assert.AreEqual(TimeSpan.FromMinutes(60 + 53), timeSpan);
        }

        [Test]
        public void Should_Plan_Correct_Retrieval_At_6_53_AM() {
            var temperature = new Mock<ITemperature>().Object;
            var dateTime = new DateTime(2016, 5, 16, 6, 53, 0);

            var timeSpan = RetrievalPlanner.Next(temperature, dateTime);

            Assert.AreEqual(TimeSpan.Zero, timeSpan);
        }

        [Test]
        public void Should_Plan_Correct_Retrieval_At_6_54_AM_With_Temperature_From_2_Minutes_Ago() {
            var temperature = new Mock<ITemperature>();
            temperature.Setup(x => x.MeasuredAt).Returns(new DateTime(2016, 5, 16, 6, 52, 0));
            var dateTime = new DateTime(2016, 5, 16, 6, 54, 0);

            var timeSpan = RetrievalPlanner.Next(temperature.Object, dateTime);

            Assert.AreEqual(TimeSpan.FromMinutes(59), timeSpan);
        }

        [Test]
        public void Should_Plan_Correct_Retrieval_At_6_54_AM_With_Temperature_From_Last_Day() {
            var temperature = new Mock<ITemperature>();
            temperature.Setup(x => x.MeasuredAt).Returns(new DateTime(2016, 5, 15, 16, 52, 0));
            var dateTime = new DateTime(2016, 5, 16, 6, 54, 0);

            var timeSpan = RetrievalPlanner.Next(temperature.Object, dateTime);

            Assert.AreEqual(TimeSpan.Zero, timeSpan);
        }

        [Test]
        public void Should_Plan_Correct_Retrieval_At_20_00_PM() {
            var temperatureMock = new Mock<ITemperature>();
            temperatureMock.Setup(x => x.MeasuredAt).Returns(new DateTime(2016, 5, 16, 16, 52, 0));
            var temperature = temperatureMock.Object;
            var dateTime = new DateTime(2016, 5, 16, 20, 00, 0);

            var timeSpan = RetrievalPlanner.Next(temperature, dateTime);

            Assert.AreEqual(TimeSpan.FromHours(10).Add(TimeSpan.FromMinutes(53)), timeSpan);
        }

        [Test]
        public void Should_Plan_Correct_Retrieval_When_Last_Measure_Is_From_Yesterday_And_Its_After_20_00_PM() {
            var temperatureMock = new Mock<ITemperature>();
            temperatureMock.Setup(x => x.MeasuredAt).Returns(new DateTime(2016, 5, 15));
            var temperature = temperatureMock.Object;
            var dateTime = new DateTime(2016, 5, 16, 20, 00, 00);

            var timeSpan = RetrievalPlanner.Next(temperature, dateTime);

            Assert.AreEqual(TimeSpan.Zero, timeSpan);
        }
    }
}