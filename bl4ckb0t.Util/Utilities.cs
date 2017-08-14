using bl4ckb0t.BotInformation;
using bl4ckb0t.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace bl4ckb0t.Util
{
	public class Utilities
	{
		//utilities for requesting and handling data about the bot
		#region DataUtils
		
		/// <summary>
		/// Adds information about the bot to the global database
		/// </summary>
		/// <param name="key">The key of the information by which it can be found</param>
		/// <param name="value">The information itself</param>
		public static void AddInformation(string key, object value)
		{
			Data.information[key] = value;
		}

		/// <summary>
		/// Provides the client of the bot
		/// </summary>
		/// <returns>The client of the bot</returns>
		public static Bot Client()
		{
			return (Bot)GetInformation("client");
		}

		/// <summary>
		/// Gets the path of the data this application is storing
		/// </summary>
		/// <returns>The path</returns>
		public static string DataPath()
		{
			return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "data");
		}

		/// <summary>
		/// Gets information about the bot from the global database as an object. You need to cast yourself
		/// </summary>
		/// <param name="key">They key of the information to get</param>
		/// <returns>The information</returns>
		public static object GetInformation(string key)
		{
			return Data.information[key];
		}

		/// <summary>
		/// Gets information about the bot from the global database as a boolean
		/// </summary>
		/// <param name="key">They key of the information to get</param>
		/// <returns>The information</returns>
		public static bool GetInformationAsBool(string key)
		{
			return (bool)Data.information[key];
		}

		/// <summary>
		/// Gets information about the bot from the global database as an integer
		/// </summary>
		/// <param name="key">They key of the information to get</param>
		/// <returns>The information</returns>
		public static int GetInformationAsInt(string key)
		{
			return (int)Data.information[key];
		}

		/// <summary>
		/// Gets information about the bot from the global database as a string
		/// </summary>
		/// <param name="key">They key of the information to get</param>
		/// <returns>The information</returns>
		public static string GetInformationAsString(string key)
		{
			return (string)Data.information[key];
		}

		/// <summary>
		/// Gets information about the bot from the global database as a list of strings
		/// </summary>
		/// <param name="key">They key of the information to get</param>
		/// <returns>The information</returns>
		public static List<string> GetInformationAsStringlist(string key)
		{
			return (List<string>)Data.information[key];
		}

		/// <summary>
		/// Provides the DEFAULT name of the bot
		/// </summary>
		/// <returns>The DEFAULT name of the bot</returns>
		public static string Name()
		{
			return GetInformationAsString("name");
		}

		/// <summary>
		/// Provides the prefix of the bot
		/// </summary>
		/// <returns>The prefix of the bot</returns>
		public static string Prefix()
		{
			return GetInformationAsString("prefix");
		}

		/// <summary>
		/// Provides the version of the bot
		/// </summary>
		/// <returns>The name of the bot</returns>
		public static string Version()
		{
			return GetInformationAsString("version");
		}

		/// <summary>
		/// Provides the wip status of the bot (started with the wip parameter)
		/// </summary>
		/// <returns>true if the bot was started as wip, false otherwise</returns>
		public static bool Wip()
		{
			return GetInformationAsBool("wip");
		}

		#endregion

		//utilities for strings
		#region StringUtils

		/// <summary>
		/// Concatenates an array into a string, starting at the start of the array, seperated by a space
		/// </summary>
		/// <param name="o"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		public static string ConcatAt(object[] oA)
		{
			return ConcatAt(oA, 0, " ");
		}

		/// <summary>
		/// Concatenates an array into a string, starting at the given index of the array, seperated by a space
		/// </summary>
		/// <param name="o"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		public static string ConcatAt(object[] oA, int i)
		{
			return ConcatAt(oA, i, " ");
		}

		/// <summary>
		/// Concatenates an array into a string, starting at the start of the array, seperated by a separator
		/// </summary>
		/// <param name="o">The array to concatenate</param>
		/// <param name="separator">The string to separate the array concatenations with</param>
		/// <returns></returns>
		public static string ConcatAt(object[] oA, string separator)
		{
			return ConcatAt(oA, 0, separator);
		}

		/// <summary>
		/// Concatenates an array into a string, starting at the given index of the array, seperated by a separator
		/// </summary>
		/// <param name="o"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		public static string ConcatAt(object[] oA, int i, string separator)
		{
			string result = "";

			for(; i < oA.Length; i++)
			{
				result += oA[i].ToString() + separator;
			}

			return result.Trim();
		}

		/// <summary>
		/// Checks if two strings are equal while ignoring the casing
		/// </summary>
		/// <param name="s1">The first string to compare</param>
		/// <param name="s2">The second string to compare</param>
		public static bool EqualsIgnoreCase(string s1, string s2)
		{
			return s1.Equals(s2, StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Extracts data out of a line of XML
		/// Example: An input of "<input>Hi</input>" would lead to the output being "Hi"
		/// </summary>
		/// <param name="s">The line to extract the data from</param>
		/// <returns>The extracted data</returns>
		public static string ExtractXML(string s)
		{
			return s.Split('>')[1].Split('<')[0];
		}

		/// <summary>
		/// Matches a complete string against a regular expression. Does not ignore casing
		/// </summary>
		/// <param name="line">The string to match</param>
		/// <param name="regex">The regular expression to check</param>
		/// <returns>true if the complete string matches against the regex, false otherwise</returns>
		public static bool Matches(string line, string regex)
		{
			return Regex.IsMatch(line, $@"\A{regex}\z", RegexOptions.None);
		}

		/// <summary>
		/// Splits a string by a multiple character delimiter
		/// </summary>
		/// <param name="s">The string to split</param>
		/// <param name="split">The delimiter to split with (by?)</param>
		/// <returns></returns>
		public static string[] Split(string s, string split)
		{
			return s.Split(new[] { split }, StringSplitOptions.None);
		}

		/// <summary>
		/// Checks wether a string starts with a number
		/// </summary>
		/// <param name="s">The string to check</param>
		/// <returns>true if the string starts with a number, false otherwise</returns>
		public static bool StartsWithNumber(string s)
		{
			return s.StartsWith("1") || s.StartsWith("2") || s.StartsWith("3") || s.StartsWith("4") || s.StartsWith("5") || s.StartsWith("6") || s.StartsWith("7") || s.StartsWith("8") || s.StartsWith("9") || s.StartsWith("0");
		}

		/// <summary>
		/// Creates a substring of a given string, starting at index start, and ending at index end-1
		/// </summary>
		/// <param name="s">The string to create a substring of</param>
		/// <param name="start">The index to start (inclusive)</param>
		/// <param name="end">The index to end (exclusive)</param>
		/// <returns></returns>
		public static string Substring(string s, int start, int end)
		{
			return s.Substring(start, end - start);
		}

		/// <summary>
		/// Splits a command into its arguments leaving out the command trigger
		/// </summary>
		/// <param name="message">The message containing the command</param>
		/// <returns>The arguments of the command in a string array</returns>
		public static string[] ToArgs(string message)
		{
			string[] previous = message.Split(' ');
			string[] result = new string[previous.Length - 1];

			for(int i = 1; i < previous.Length; i++)
			{
				result[i - 1] = previous[i];
			}

			return result;
		}

		/// <summary>
		/// Applies several transformations to a resource string:
		/// Colors (&0-9 &a-k)
		/// Command prefix (%cmd%)
		/// Arguments ({n} n is the n+1th argument, e.g. {0} will be the first argument in the resource string)
		/// </summary>
		/// <param name="key">The resource string to transform</param>
		/// <param name="args">The arguments within the string to replace</param>
		/// <returns>The transformed string</returns>
		public static string Transform(string key, params object[] args)
		{
			key = key.Replace("&0", Colors.WHITE)
					.Replace("&1", Colors.BLACK)
					.Replace("&2", Colors.DARK_BLUE)
					.Replace("&3", Colors.DARK_GREEN)
					.Replace("&4", Colors.RED)
					.Replace("&5", Colors.BROWN)
					.Replace("&6", Colors.PURPLE)
					.Replace("&7", Colors.OLIVE)
					.Replace("&8", Colors.YELLOW)
					.Replace("&9", Colors.GREEN)
					.Replace("&a", Colors.TEAL)
					.Replace("&b", Colors.CYAN)
					.Replace("&c", Colors.BLUE)
					.Replace("&d", Colors.MAGENTA)
					.Replace("&e", Colors.DARK_GRAY)
					.Replace("&f", Colors.LIGHT_GRAY)
					.Replace("&g", Colors.NORMAL)
					.Replace("&h", Colors.BOLD)
					.Replace("&i", Colors.UNDERLINE)
					.Replace("&j", Colors.REVERSE)
					.Replace("&k", Colors.ITALICS)
					.Replace("%cmd%", Prefix());

			return args == null ? key : string.Format(key, args);
		}

		#endregion

		//utilities for interacting with IRC
		#region IRCUtils
		
		/// <summary>
		/// Checks if the bot has joined the given channel
		/// </summary>
		/// <param name="channel">The channel to check</param>
		/// <returns>true if the channel has been joined, false otherwise</returns>
		public static bool HasJoinedChannel(string channel)
		{
			return Client().User.Channels.Contains(channel);
		}

		/// <summary>
		/// Checks if a given user has permission level 3
		/// </summary>
		/// <param name="nick">The nick of the user to check</param>
		/// <returns>true if they have permission level 3, false otherwise</returns>
		public static bool IsLvl3User(string nick)
		{
			return Lists.GetLvl3Users().Contains(nick.ToLower());
		}

		/// <summary>
		/// Sends a message to a user or channel and transforms it prior to sending using Utilities.Transform()
		/// </summary>
		/// <param name="target">The user or channel to send the message to</param>
		/// <param name="msg">The message to send</param>
		/// <param name="args">These are the arguments for the transformation</param>
		public static void SendMessage(string target, string msg, params object[] args)
		{
			msg = Transform(msg, args);
			Client().SendMessage(msg, target);
			Logger.Message(target, Name(), msg);
		}

		/// <summary>
		/// Sends a message with some data, seperated by two asterisks (*)
		/// ** data[0] ** data[1] ** ... ** data[data.Length - 1] **
		/// Each new part resets colors and is prefixed with Colors.BOLD 
		/// </summary>
		/// <param name="channel">The channel to send the message to</param>
		/// <param name="data">The data to display in the message</param>
		public static void SendStarMsg(string channel, params string[] data)
		{
			string result = Colors.NORMAL;

			foreach(string s in data)
			{
				result += Colors.NORMAL + Colors.BOLD + " ** " + s;
			}

			SendMessage(channel, result.Trim() + Colors.NORMAL + Colors.BOLD + " **");
		}

		#endregion

		//utilities for requesting and handling specific time data about the bot
		#region TimeUtils
		
		/// <summary>
		/// Gets the build date of an assembly
		/// </summary>
		/// <param name="assembly">The assembly</param>
		/// <returns>The build date</returns>
		public static DateTime BuildDate()
		{
			var filePath = Assembly.GetEntryAssembly().Location;
			int peHeaderOffset = 60;
			int linkerTimestampOffset = 8;
			var buffer = new byte[2048];

			using(var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				stream.Read(buffer, 0, 2048);
			}

			var offset = BitConverter.ToInt32(buffer, peHeaderOffset);
			var secondsSince1970 = BitConverter.ToInt32(buffer, offset + linkerTimestampOffset);
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

			return TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, TimeZoneInfo.Local);
		}

		/// <summary>
		/// Gets the time in milliseconds since 01.01.1970
		/// </summary>
		/// <returns>The time</returns>
		public static long CurrentTimeMillis()
		{
			return ToTimestamp(DateTime.UtcNow);
		}

		/// <summary>
		/// Changes a string of the format xdxhxmxs (where x is a positive number) to a timestamp
		/// </summary>
		/// <param name="s">The string to convert</param>
		/// <returns>The converted string in milliseconds, -1 if malformed</returns>
		public static long FormatToUnix(string s)
		{
			if(s.Contains("d"))
				return ConvertDays(s);
			else if(s.Contains("h"))
				return ConvertHours(s);
			else if(s.Contains("m"))
				return ConvertMinutes(s);
			else if(s.Contains("s"))
				return ConvertSeconds(s);
			else
				return -1;
		}

		/// <summary>
		/// Changes a string of the format xdxhxmxs (where x is a positive number) to a TimeStamp object
		/// </summary>
		/// <param name="s">The string to convert</param>
		/// <returns>The TimeSpan object, a 0 TimeSpan if there was an error</returns>
		public static TimeSpan ToTimeSpan(string s)
		{
			try
			{
				int days = 0;
				int hours = 0;
				int minutes = 0;
				int seconds = 0;

				if(s.Contains("d"))
				{
					if(s.Contains("h"))
					{
						if(s.Contains("m"))
						{
							if(s.Contains("s"))
								seconds = int.Parse(s.Split('m')[1].Split('s')[0]);

							minutes = int.Parse(s.Split('h')[1].Split('m')[0]);
						}

						hours = int.Parse(s.Split('d')[1].Split('h')[0]);
					}

					days = int.Parse(s.Split('d')[0]);
				}
				else if(s.Contains("h"))
				{
					if(s.Contains("m"))
					{
						if(s.Contains("s"))
							seconds = int.Parse(s.Split('m')[1].Split('s')[0]);

						minutes = int.Parse(s.Split('h')[1].Split('m')[0]);
					}

					hours = int.Parse(s.Split('h')[0]);
				}
				else if(s.Contains("m"))
				{
					if(s.Contains("s"))
						seconds = int.Parse(s.Split('m')[1].Split('s')[0]);

					minutes = int.Parse(s.Split('m')[0]);
				}
				else if(s.Contains("s"))
					seconds = int.Parse(s.Split('s')[0]);

				return new TimeSpan(days, hours, minutes, seconds);
			}
			catch(Exception)
			{
				return new TimeSpan(0, 0, 0, 0, 0);
			}
		}

		/// <summary>
		/// Converts a DateTime to a UNIX timestamp
		/// </summary>
		/// <param name="time">The DateTime object to convert</param>
		/// <returns>The UNIX timestamp</returns>
		public static long ToTimestamp(DateTime time)
		{
			return (long)time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
		}

		/// <summary>
		/// Changes a timestamp into a String with the given format
		/// </summary>
		/// <param name="stamp">The timestamp in milliseconds to convert</param>
		/// <param name="format">The format the String will have
		/// <returns>The converted timestamp as a String</returns>
		public static string UnixToFormat(long stamp, string format)
		{
			long s = stamp / 1000;
			long m = s / 60;
			long h = m / 60;
			long d = h / 24;

			s -= 60 * m;
			m -= 60 * h;
			h -= 24 * d;
			d = Math.Abs(d);
			h = Math.Abs(h);
			m = Math.Abs(m);
			s = Math.Abs(s);
			return string.Format(format, (d < 10 ? "0" + d : "" + d), (h < 10 ? "0" + h : "" + h), (m < 10 ? "0" + m : "" + m), (s < 10 ? "0" + s : "" + s));
		}

		/// <summary>
		/// Gets the current running time of this program
		/// </summary>
		/// <returns>The current running time of this program in milliseconds</returns>
		public static long Uptime()
		{
			return ToTimestamp(DateTime.Now) - ToTimestamp(Process.GetCurrentProcess().StartTime);
		}

		#endregion

		//utilities for file handling
		#region FileUtils
		
		/// <summary>
		/// Clears the content of a file
		/// </summary>
		/// <param name="path">The path to the file to clear</param>
		public static void ClearFile(string path)
		{
			File.WriteAllText(path, string.Empty);
		}

		/// <summary>
		/// Reads a file and puts each line into a list entry
		/// This does not close the StreamReader
		/// </summary>
		/// <param name="reader">The reader of the file to read</param>
		/// <param name="close">true if the reader should be closed, false otherwise</param>
		/// <returns>A list with all lines in the file</returns>
		public static List<string> ReadLines(StreamReader reader, bool close = false)
		{
			List<string> list = new List<string>();
			string line = "";

			while((line = reader.ReadLine()) != null)
			{
				list.Add(line);
			}

			if(close)
				reader.Close();

			return list;
		}

		/// <summary>
		/// Writes lines to a file
		/// </summary>
		/// <param name="writer">The writer of the file to write</param>
		/// <param name="lines">The lines to write</param>
		/// <param name="close">true if the writer should be closed, false otherwise</param>
		public static void WriteLines(StreamWriter writer, List<string> lines, bool close = false)
		{
			foreach(string s in lines)
			{
				writer.WriteLine(s);
			}

			writer.Flush();

			if(close)
				writer.Close();
		}

		#endregion

		//utilities for dealing with numbers
		#region NumberUtils
		
		/// <summary>
		/// Formats a double to two decimal places
		/// </summary>
		/// <param name="celsius">The double to format</param>
		/// <returns>The formatted double</returns>
		public static double FormatDouble(double d)
		{
			return Math.Round(d, 2);
		}

		#endregion

		//methods used only by the Utilities class
		#region PrivateMethods

		/// <summary>
		/// Sub-method of FormatToUnix() handling days
		/// </summary>
		/// <param name="s">The string to convert</param>
		/// <returns>The converted string</returns>
		private static long ConvertDays(string s)
		{
			long value = long.Parse(s.Split('d')[0]);

			value *= 24 * 60 * 60;

			if(s.Contains("h"))
				return value + ConvertHours(s);
			else if(s.Contains("m"))
				return value + ConvertMinutes(s);
			else if(s.Contains("s"))
				return value + ConvertSeconds(s);
			else
				return value;
		}

		/// <summary>
		/// Sub-method of FormatToUnix() handling hours
		/// </summary>
		/// <param name="s">The string to convert</param>
		/// <returns>The converted string</returns>
		private static long ConvertHours(string s)
		{
			long value = 0;

			if(s.Contains("d"))
				value = long.Parse(s.Split('d')[1].Split('h')[0]);
			else
				value = long.Parse(s.Split('h')[0]);

			value *= 60 * 60;

			if(s.Contains("m"))
				return value + ConvertMinutes(s);
			else if(s.Contains("s"))
				return value + ConvertSeconds(s);
			else
				return value;
		}

		/// <summary>
		/// Sub-method of FormatToUnix() handling minutes
		/// </summary>
		/// <param name="s">The string to convert</param>
		/// <returns>The converted string</returns>
		private static long ConvertMinutes(string s)
		{
			long value = 0;

			if(s.Contains("d"))
			{
				if(s.Contains("h"))
					value = long.Parse(s.Split('d')[1].Split('h')[1].Split('m')[0]);
				else
					value = long.Parse(s.Split('d')[1].Split('m')[0]);
			}
			else if(s.Contains("h"))
				value = long.Parse(s.Split('h')[1].Split('m')[0]);
			else
				value = long.Parse(s.Split('m')[0]);

			value *= 60;

			if(s.Contains("s"))
				return value + ConvertSeconds(s);
			else
				return value;
		}

		/// <summary>
		/// Sub-method of FormatToUnix() handling seconds
		/// </summary>
		/// <param name="s">The string to convert</param>
		/// <returns>The converted string</returns>
		private static long ConvertSeconds(string s)
		{
			long value = 0;

			if(s.Contains("d"))
			{
				if(s.Contains("h"))
				{
					if(s.Contains("m"))
						value = long.Parse(s.Split('d')[1].Split('h')[1].Split('m')[1].Split('s')[0]);
					else
						value = long.Parse(s.Split('d')[1].Split('h')[1].Split('s')[0]);
				}
				else
				{
					if(s.Contains("m"))
						value = long.Parse(s.Split('d')[1].Split('m')[1].Split('s')[0]);
					else
						value = long.Parse(s.Split('d')[1].Split('s')[0]);
				}
			}
			else if(s.Contains("h"))
			{
				if(s.Contains("m"))
					value = long.Parse(s.Split('h')[1].Split('m')[1].Split('s')[0]);
				else
					value = long.Parse(s.Split('h')[1].Split('s')[0]);
			}
			else if(s.Contains("m"))
				value = long.Parse(s.Split('m')[1].Split('s')[0]);
			else
				value = long.Parse(s.Split('s')[0]);

			return value;
		}

		#endregion
	}
}
