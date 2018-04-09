using bl4ckb0t.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace bl4ckb0t.Logging
{
    public class Logger
	{
		private static readonly List<string> buffer = new List<string>();
		private static StreamWriter writer;
		private static bool enabled = false;

		/// <summary>
		/// Creates a new .log file and copies the old one into an archive
		/// </summary>
		public static void Setup()
		{
			string botname = Utilities.Name();
			string logfolder = Path.Combine(Utilities.DataPath(), "logs");
			string x = Path.Combine(logfolder, $"{botname}.log");
			Uri uri = new Uri(x);
			string logfile = uri.LocalPath;

			Directory.CreateDirectory(new Uri(logfolder).LocalPath);

			if(!File.Exists(logfile))
			{
				buffer.Add($"\"{botname}.log\" does not exist, creating new file");
				File.Create(logfile).Close();
				buffer.Add("File succesfully created");
			}
			else
			{
				FileStream orig = File.OpenRead(logfile);
				FileStream copy = File.Create(new Uri(Path.Combine(logfolder, $"{DateTime.Now.ToString().Replace(':', '-')}.log")).LocalPath);

				buffer.Add($"Created new file to copy old logs to: \"{copy.Name}\"");
				buffer.Add("Starting copy process");
				orig.CopyTo(copy);
				buffer.Add("Successfully copied old logging file");
				copy.Close();
				orig.Close();
			}

			Utilities.ClearFile(logfile);
			writer = new StreamWriter(File.OpenWrite(logfile));
			Enable();
		}

		/// <summary>
		/// Enables this logger
		/// </summary>
		public static void Enable()
		{
			enabled = true;
			Start();
		}

		/// <summary>
		/// Disables this logger
		/// </summary>
		public static void Disable()
		{
			End();
			enabled = false;
		}

		/// <summary>
		/// Starts this logger
		/// </summary>
		private static void Start()
		{
			Raw($"Started logging on {DateTime.Now.ToString()}");

			foreach(string s in buffer)
			{
				Info(s);
			}

			buffer.Clear();
		}

		/// <summary>
		/// Stops this logger
		/// </summary>
		private static void End()
		{
			Raw($"Ended logging on {DateTime.Now.ToString()}");
		}

		/// <summary>
		/// Logs a raw line, without a timestamp
		/// </summary>
		/// <param name="line">The line to log</param>
		private static void Raw(object line)
		{
			if(enabled)
			{
				writer.WriteLine(line);
				writer.Flush();
				Console.WriteLine(line);
			}
		}

		/// <summary>
		/// Logs a line with a timestamp prefix
		/// </summary>
		/// <param name="line">The line to log</param>
		private static void Log(object line)
		{
			if(enabled)
			{
				writer.WriteLine($"{DateTime.Now.ToString()} {Colors.RemoveColorsAndFormatting(line.ToString())}");
				writer.Flush();
				Console.WriteLine($"{DateTime.Now.ToString()} {Colors.RemoveColorsAndFormatting(line.ToString())}");
			}
		}

		/// <summary>
		/// Logs a stack trace
		/// </summary>
		/// <param name="e">The thrown exception of which to log the stack trace</param>
		public static void StackTrace(Exception e)
		{
			Raw(e.ToString());
			Raw(e.StackTrace.ToString());
		}

		/// <summary>
		/// Logs a severe event (prefix: [SEVERE])
		/// </summary>
		/// <param name="line">The line to log</param>
		public static void Severe(object line)
		{
			Log($"[SEVERE] {line}");
		}

		/// <summary>
		/// Logs a warning (prefix: [WARNING])
		/// </summary>
		/// <param name="line">The line to log</param>
		public static void Warn(object line)
		{
			Log($"[WARNING] {line}");
		}

		/// <summary>
		/// Logs a normal line (prefix: [INFO])
		/// </summary>
		/// <param name="line">The line to log</param>
		public static void Info(object line)
		{
			Log($"[INFO] {line}");
		}

		/// <summary>
		/// Logs a normal line, only when the bot is in work in progress mode (aka bl4ckb0t.Core.wip = true) (prefix: [DEBUG])
		/// </summary>
		/// <param name="line">The line to log</param>
		public static void Debug(object line)
		{
			if(Utilities.Wip())
				Log($"[DEBUG] {line}");
		}

		/// <summary>
		/// Logs an IRC message (prefix: [MESSAGE])
		/// </summary>
		/// <param name="target">The channel the message got sent to</param>
		/// <param name="sender">The sender of the message</param>
		/// <param name="msg">The message</param>
		public static void Message(string target, string sender, string msg)
		{
			Log($"[MESSAGE] {target} | {sender}: {msg}");
		}

		/// <summary>
		/// Logs an IRC notice sent to someone (prefix: [NOTICE])
		/// </summary>
		/// <param name="receiver">The user who received the notice</param>
		/// <param name="notice">The notice content</param>
		public static void NoticeSent(string receiver, string notice)
		{
			Log($"[NOTICE] {receiver} | {Utilities.Name()}: {notice}");
		}

		/// <summary>
		/// Logs an IRC notice that got sent to the bot (prefix: [NOTICE])
		/// </summary>
		/// <param name="sender">The sender of the notice</param>
		/// <param name="notice">The notice content</param>
		public static void NoticeReceived(string sender, string notice)
		{
			Log($"[NOTICE] {sender}: {notice}");
		}

		/// <summary>
		/// Logs an IRC pm (prefix: [PM])
		/// </summary>
		/// <param name="sender">The sender of the pm</param>
		/// <param name="pm">The pm's content</param>
		public static void Pm(string sender, string pm)
		{
			Log($"[PM] {sender}: {pm}");
		}

		/// <summary>
		/// Logs an IRC action (prefix: [ACTION])
		/// </summary>
		/// <param name="target">The channel or user the action got sent to</param>
		/// <param name="sender">The sender of the action</param>
		/// <param name="action">The action's content</param>
		public static void Action(string target, string sender, string action)
		{
			Log($"[ACTION] {target} | {sender} {action}");
		}

		/// <summary>
		/// Registers all listeners used for logging IRC data to the client
		/// </summary>
		/// <param name="client">The client to register the listeners to</param>
		public static void AddListeners(Bot client)
		{
			client.ChannelMessageRecieved += (c, e) => {
				if(e.PrivateMessage.IsAction)
					Action(e.PrivateMessage.Source, e.PrivateMessage.User.Nick, e.PrivateMessage.Message);
				else
					Message(e.PrivateMessage.Source, e.PrivateMessage.User.Nick, e.PrivateMessage.Message);
			};

			client.NoticeRecieved += (c, e) => {
				NoticeReceived(e.Source, e.Notice);
			};

			client.UserKicked += (c, e) => {
				if(e.Kicked.Nick.Equals(Utilities.Name()))
					Warn($"Bot kicked from {e.Channel.Name} by {e.Kicker.Nick} for \"{e.Reason}\"");
				else
					Info($"{e.Kicked.Nick} was kicked from {e.Channel.Name} by {e.Kicker.Nick} for \"{e.Reason}\"");
			};

			client.NickChanged += (c, e) => {
				Info($"{e.OldNick} changed their name to {e.NewNick}");
			};

			client.UserPartedChannel += (c, e) => {
				Info($"{e.User.Nick} left {e.Channel.Name}");
			};

			client.UserJoinedChannel += (c, e) => {
				if(!e.User.Nick.Equals(Utilities.Name()))
					Info($"{e.User.Nick} joined {e.Channel.Name}");
			};

			client.UserQuit += (c, e) => {
				Info($"{e.User.Nick} quit");
			};

			client.UserMessageRecieved += (c, e) => {
				if(e.PrivateMessage.Message.Contains("ACTION"))
					Action(e.PrivateMessage.Source, e.PrivateMessage.User.Nick, e.PrivateMessage.Message.Replace("", "").Replace("ACTION", "").Trim());
				else
					Pm(e.PrivateMessage.Source, e.PrivateMessage.Message);
			};

			client.NetworkError += (c, e) => {
				Severe("Lost connection to server!");
			};
		}
	}
}
