using NUnit.Framework;
using MHTemperature.Contracts;

namespace MHTemperature.Tests
{
	[TestFixture]
	public class TemperatureServiceTest
	{
		/// <summary>
		/// NOTE: Assert that you are connect to the internet when running test.
		/// </summary>
		[Test]
		public void GetCurrentTemperature()
		{
			var service = new TemperatureService();
			var temperature = service.Current();

			Assert.IsInstanceOf(typeof(ITemperatureService), service);
			Assert.NotNull(temperature);
			Assert.NotNull(temperature.Swimmer);
			Assert.NotNull(temperature.NonSwimmer);
			Assert.NotNull(temperature.KidSplash);
			Assert.NotNull(temperature.DateTime);
		}
	}
}