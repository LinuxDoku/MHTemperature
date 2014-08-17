using System;
using Humanizer;

namespace MHTemperature
{
	public class Formatter
	{
		public static string FormatTemperature(float value) {
			return value.ToString("F") + " °C";
		}

		public static string FormatDateTime(DateTime dateTime) {
			return dateTime.Humanize(false);
		}
	}
}

