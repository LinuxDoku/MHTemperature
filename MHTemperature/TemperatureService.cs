using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;
using MHTemperature.Contracts;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MHTemperature {
    public class TemperatureService : ITemperatureService {
        public const string WebServiceUrl = "http://www.stadt.noerdlingen.de/phpSkripte/temp_freibad/temp_freibad_neu.php";

        /// <summary>
        /// Get the current temperature.
        /// </summary>
        public ITemperature Current() {
            string html;

            try {
                html = GetHtml();
            }
            catch (Exception ex) {
                throw new Exception("Could not receive html from webservice!", ex);
            }

            try {
                var result = ParseHtml(html);
                return result;
            }
            catch (Exception ex) {
                throw new Exception($"Could not parse html from webservice! {html}!", ex);
            }
        }

        /// <summary>
        /// Get the data from webservice.
        /// </summary>
        /// <returns>The html document.</returns>
        private string GetHtml() {
            var httpClient = new HttpClient();
            return httpClient.GetStringAsync(WebServiceUrl).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Ugly parsing of the html document.
        /// </summary>
        /// <returns>The parsed temperature.</returns>
        /// <param name="html">Html document as string.</param>
        private ITemperature ParseHtml(string html) {
            if (string.IsNullOrEmpty(html)) {
                return null;
            }

            var resultTemperature = new Temperature();

            // create an html document and parse the structure
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var rows = document.DocumentNode.Descendants("tr");

            // the layout is build on tables
            if (rows.Count() >= 6) {
                // time
                var timeRow = rows.First();
                resultTemperature.MeasuredAt = ParseDateTime(timeRow.Descendants("b").Last().InnerText);

                // temperatures
                var temperatureRows = rows.Skip(3).Take(3);

                for (var i = 0; i < temperatureRows.Count(); i++) {
                    var row = temperatureRows.ElementAt(i);
                    var value = ParseTemperature(row.Descendants("b").Last().InnerText);

                    switch (i) {
                        case 0:
                            resultTemperature.Swimmer = value;
                            break;
                        case 1:
                            resultTemperature.NonSwimmer = value;
                            break;
                        case 2:
                            resultTemperature.KidSplash = value;
                            break;
                    }
                }
            }

            return resultTemperature;
        }

        /// <summary>
        /// Parse the last update timestamp.
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="dateTime">Date time.</param>
        public DateTime ParseDateTime(string dateTime) {
            // "16.8.2014, 12:56 Uhr"
            dateTime = dateTime.Replace(" Uhr", "");
            dateTime = dateTime.Replace(", ", "");

            var matches = Regex.Matches(dateTime, "(?<day>[0-9]{1,2}).(?<month>[0-9]{1,2}).(?<year>[0-9]{4}) (?<hour>[0-9]{1,2}):(?<minute>[0-9]{1,2})", RegexOptions.Compiled);
            return new DateTime(GetInt(matches, "year"), GetInt(matches, "month"), GetInt(matches, "day"), GetInt(matches, "hour"), GetInt(matches, "minute"), 0);
        }

        /// <summary>
        /// Get an int from the given group in the match collection.
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
	    private int GetInt(MatchCollection matches, string groupName) {
            return int.Parse(matches[0].Groups[groupName].Value);
        }

        /// <summary>
        /// Parsing of the value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="value">Value.</param>
        private float ParseTemperature(string value) {
            var result = Regex.Match(value, @"([0-9]{1,2}\,[0-9]{1,2})").Value;
            return float.Parse(result, CultureInfo.GetCultureInfoByIetfLanguageTag("de")); // force "de" on machines with other culture
        }
    }
}

