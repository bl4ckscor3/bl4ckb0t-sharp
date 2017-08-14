using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using NSoup.Nodes;
using System;

namespace bl4ckb0t.Modules.Weather
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length > 0)
			{
				string channel = e.Source;
				string city = "";

				foreach(string s in args)
				{
					city += $"{s} ";
				}

				if(city.Equals(""))
					return false;

				city = city.Trim();

				try
				{
					Document doc = WebWrapper.NewNSoup($"http://api.openweathermap.org/data/2.5/weather?q={city}&mode=xml&APPID={Passwords.weatherapikey}", true);

					Utilities.SendStarMsg(channel,
						$"{doc.Select("city").Attr("name")}, {doc.Select("country").Text}",
						Utilities.Transform(Resources.temperature, GetTemperature(doc)),
						Utilities.Transform(Resources.humidity, doc.Select("humidity").Attr("value") + doc.Select("humidity").Attr("unit")),
						Utilities.Transform(Resources.pressure, doc.Select("pressure").Attr("value") + doc.Select("pressure").Attr("unit")),
						Utilities.Transform(Resources.wind, GetWindSpeed(doc)),
						Utilities.Transform(Resources.updated, $"{doc.Select("lastupdate").Attr("value").Replace("T", " ")} GMT"),
						Utilities.Transform(Resources.more, $"http://www.openweathermap.org/city/{doc.Select("city").Attr("id")}"));
				}
				catch(Exception ex)
				{
					Logging.Logger.StackTrace(ex);
					Utilities.SendMessage(channel, Resources.cityNotFound, city, ":/");
				}

				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Gets the temperature from the opened OpenWeatherMap API page
		/// </summary>
		/// <param name="doc">The document with the opened API page</param>
		/// <returns>The temperature x, formatted as x°C | x°F | xK</returns>
		private string GetTemperature(Document doc)
		{
			double kelvin = double.Parse(doc.Select("temperature").Attr("value"));
			double celsius = kelvin - 273.15D;

			return $"{Utilities.FormatDouble(celsius)}°C | {Utilities.FormatDouble(celsius * (9D / 5D) + 32D)}°F | {kelvin}K";
		}

		/// <summary>
		/// Gets the windspeed from the opened OpenWeatherMap API page
		/// </summary>
		/// <param name="doc">The document with the opened API page</param>
		/// <returns>The windspeed x with direction y, formatted as x m/s | x mph y</returns>
		private string GetWindSpeed(Document doc)
		{
			double ms = double.Parse(doc.Select("speed").Attr("value"));

			return $"{ms} m/s | {Utilities.FormatDouble(ms * 2.2369362920544)} mph {doc.Select("direction").Attr("code")}";
		}

		public override string[] Aliases()
		{
			return new string[] { "weather", "w" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
