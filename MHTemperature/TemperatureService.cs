using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using MHTemperature.Contracts;
using System.Globalization;
using System.Security.AccessControl;

namespace MHTemperature
{
	public class TemperatureService
	{
		public const string WebServiceUrl = "http://www.stadt.noerdlingen.de/sonstige/temp_freibad/temp_freibad.php";

		/// <summary>
		/// Get the current temperature.
		/// </summary>
		public ITemperature Current() {
			return ParseData(GetData());
		}

		/// <summary>
		/// Get the data from webservice.
		/// </summary>
		/// <returns>The html document.</returns>
		protected string GetData() {
			var httpClient = new HttpClient();
			return httpClient.GetStringAsync(WebServiceUrl).GetAwaiter().GetResult();
		}

		/// <summary>
		/// Ugly parsing of the html document.
		/// </summary>
		/// <returns>The parsed temperature.</returns>
		/// <param name="data">Html document as string.</param>
		protected ITemperature ParseData(string data) {
			if(data != null) {
				var resultTemperature = new Temperature();
				var document = new HtmlDocument();
				document.LoadHtml(data);
				var rows = document.DocumentNode.Descendants("tr");

				if(rows.Count() >= 6) {
					// time
					var timeRow = rows.First();
					resultTemperature.DateTime = ParseDateTime(timeRow.Descendants("b").Last().InnerText);


					// temperatures
					var temperatureRows = rows.Skip(3).Take(3);

					for(var i = 0; i < temperatureRows.Count(); i++) {
						var row = temperatureRows.ElementAt(i);
						var value = ParseTemperature(row.Descendants("b").Last().InnerText);

						if(i == 0) {
							resultTemperature.Swimmer = value;
						} else if(i == 1) {
							resultTemperature.NonSwimmer = value;
						} else {
							resultTemperature.KidSplash = value;
						}
					}
				}

				return resultTemperature;
			}

			return null;
		}

		/// <summary>
		/// Parse the last update timestamp.
		/// </summary>
		/// <returns>The date time.</returns>
		/// <param name="dateTime">Date time.</param>
		protected DateTime ParseDateTime(string dateTime) {
			// "16.8.2014, 12:56 Uhr"
			dateTime = dateTime.Replace(" Uhr", "");
			dateTime = dateTime.Replace(", ", "");

			// "16.8.2014 12:56"
			return DateTime.ParseExact(dateTime, "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Parsing of the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="value">Value.</param>
		protected float ParseTemperature(string value) {
			value = value.Replace(" °C", "");
			return Single.Parse(value, CultureInfo.GetCultureInfoByIetfLanguageTag("de")); // force "de" on machines with other culture
		}
	}
}

