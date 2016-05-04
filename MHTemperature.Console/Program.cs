using System;
using System.Globalization;

namespace MHTemperature.ConsoleHost {
    public static class Program {
        public static void Main(string[] args) {
            var service = new TemperatureService();
            var temperature = service.Current();

            if (temperature != null) {
                Console.WriteLine("Aktuelle Wasser-Temperatur im Freibad Marienhöhe:");
                Console.WriteLine();
                Console.WriteLine($"Schwimmer-, Sprungbecken          {temperature.Swimmer:F2} °C");
                Console.WriteLine($"Nichtschwimmer-, Rutschenbecken   {temperature.NonSwimmer:F2} °C");
                Console.WriteLine($"Kinderbecken                      {temperature.KidSplash:F2} °C");
                Console.WriteLine();
                Console.WriteLine($"Zuletzt aktualisiert {temperature.DateTime.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("de"))}");
                Console.ReadLine();
            } else {
                Console.WriteLine("Huch, die aktuelle Wassertemperatur konnte leider nicht abgerufen werden.");
            }
        }
    }
}