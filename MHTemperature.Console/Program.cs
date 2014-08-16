using System;
using System.Globalization;

namespace MHTemperature.ConsoleHost
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var service = new TemperatureService();
			var temperature = service.Current();

			if(temperature != null) {
				Console.WriteLine("Aktuelle Wasser-Temperatur im Freibad Marienhöhe:");
				Console.WriteLine();
				Console.WriteLine("Schwimmer-, Sprungbecken          {0:F2} °C", temperature.Swimmer);
				Console.WriteLine("Nichtschwimmer-, Rutschenbecken   {0:F2} °C", temperature.NonSwimmer);
				Console.WriteLine("Kinderbecken                      {0:F2} °C", temperature.KidSplash);
				Console.WriteLine();
				Console.WriteLine("Zuletzt aktualisiert {0}", temperature.DateTime.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("de")));
			} else {
				Console.WriteLine("Huch, die aktuelle Wassertemperatur konnte leider nicht abgerufen werden.");
			}
		}
	}
}