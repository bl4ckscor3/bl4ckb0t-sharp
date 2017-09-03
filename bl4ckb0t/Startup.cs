using bl4ckb0t.BotInformation;
using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Modules;
using bl4ckb0t.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Globalization;
using System.Reflection;

namespace bl4ckb0t
{
	public class Startup
	{
		/// <summary>
		/// Sets the default culture. Only ever use this once
		/// </summary>
		public static void SetCulture()
		{
			Type type = typeof(CultureInfo);
			CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

			type.InvokeMember("s_userDefaultCulture",
								BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
								null,
								culture,
								new object[] { culture });
			type.InvokeMember("s_userDefaultUICulture",
								BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
								null,
								culture,
								new object[] { culture });
		}

		/// <summary>
		/// Fetches all external information the bot uses to function, such as permission levels and default channels
		/// </summary>
		public static void Fetch()
		{
			try
			{
				Logger.Info("Fetching changelog");
				FetchChangelog();
			}
			catch(WebException)
			{
				Logger.Warn("GitHub is not reachable. Crashes can occur and some modules may not work correctly.");
			}

			try
			{
				Logger.Info("Fetching default channels");
				FetchDefaultChannels();
				Logger.Info("Fetching level 2 users");
				FetchLvl2Users();
				Logger.Info("Fetching level 3 users");
				FetchLvl3Users();
				Logger.Info("Fetching ignored users");
				FetchIgnoredUsers();
			}
			catch(WebException)
			{
				Logger.Warn("Dropbox is not reachable. Crashes can occur and some modules may not work correctly.");
			}
		}

		private static void FetchChangelog()
		{
			WebClient client = WebWrapper.NewClient();
			StreamReader reader = new StreamReader(client.OpenRead("https://raw.githubusercontent.com/bl4ckscor3/bl4ckb0t/master/CHANGELOG.md"));
			string line = "";
			string currentVersion = "";

			while((line = reader.ReadLine()) != null)
			{
				line = line.Replace("#", "").Replace("*", "");

				if(Utilities.StartsWithNumber(line))
				{
					currentVersion = line;
					Changelog.versions[line] = new List<string>();
				}

				if(line.StartsWith("-") && !line.StartsWith("---"))
					Changelog.versions[currentVersion].Add(line);
			}

			client.Dispose();
			reader.Close();
			Logger.Info("All versions added to the changelog list");
		}

		private static void FetchDefaultChannels()
		{
			if(Utilities.Wip())
			{
				Lists.AddDefaultChannel("#bl4ckb0tTest");
				((Dictionary<string, string>)Utilities.GetInformation("langs"))["#bl4ckb0tTest"] = "en-US";
				return;
			}

			WebClient client = WebWrapper.NewClient();
			StreamReader reader = new StreamReader(client.OpenRead("https://akino.canopus.uberspace.de/bl4ckb0t/files/defaultChannels.txt"));
			string line = "";

			while((line = reader.ReadLine()) != null)
			{
				if(line.Contains(","))
					((Dictionary<string, string>)Utilities.GetInformation("langs"))[line.Split(',')[0]] = line.Split(',')[1];
				else
					((Dictionary<string, string>)Utilities.GetInformation("langs"))[line] = "en-US";

				Lists.AddDefaultChannel(line);
			}

			client.Dispose();
			reader.Close();
			Logger.Info("All channels added to the default channels list");
		}

		private static void FetchLvl2Users()
		{
			WebClient client = WebWrapper.NewClient();
			StreamReader reader = new StreamReader(client.OpenRead("https://akino.canopus.uberspace.de/bl4ckb0t/files/allowedUsers.txt"));
			string line = "";

			while((line = reader.ReadLine()) != null)
			{
				Lists.AddLvl2User(line);
			}

			client.Dispose();
			reader.Close();
			Logger.Info("All users with permission level 2 added to the list");
		}

		private static void FetchLvl3Users()
		{
			WebClient client = WebWrapper.NewClient();
			StreamReader reader = new StreamReader(client.OpenRead("https://akino.canopus.uberspace.de/bl4ckb0t/files/validUsers.txt"));
			string line = "";

			while((line = reader.ReadLine()) != null)
			{
				Lists.AddLvl3User(line);
			}

			client.Dispose();
			reader.Close();
			Logger.Info("All users with permission level 3 added to the list");
		}

		private static void FetchIgnoredUsers()
		{
			WebClient client = WebWrapper.NewClient();
			StreamReader reader = new StreamReader(client.OpenRead("https://akino.canopus.uberspace.de/bl4ckb0t/files/ignoredUsers.txt"));
			string line = "";

			while((line = reader.ReadLine()) != null)
			{
				Lists.AddIgnoredUser(line);
			}

			client.Dispose();
			reader.Close();
			Logger.Info("All ignored users added to the list");
		}

		/// <summary>
		/// Loads and initializes all private modules (aka modules that shipped with the bot)
		/// </summary>
		/// <param name="client">The client to which to register the modules to</param>
		public static void InitPrivateModules(Bot client)
		{
			BaseModule[] privateModules = {
				new Changelog("Changelog"),
				new Help("Help"),
				new Info("Info"),
				new LinkManager("LinkManager"), //this is located in the ModuleAPI project
				new ModuleManagement("ModuleManagement"),
				new Source("Source"),
				new Shutdown("Shutdown"),
				new Update("Update")
			};

			foreach(BaseModule m in privateModules)
			{
				m.OnEnable(client);
				ModuleHandler.Add(m);
				Logger.Info("Loaded module " + m.Name);
			}

		}

		/// <summary>
		/// Loads and initializes all public modules (aka modules in the \modules subfolder)
		/// </summary>
		/// <param name="client">The client to which to register the modules to</param>
		public static void InitPublicModules(Bot client)
		{
			string moduleFolder = new Uri(Path.Combine(Utilities.DataPath(), "modules")).LocalPath;

			if(!Directory.Exists(moduleFolder))
				Directory.CreateDirectory(moduleFolder);

			string[] files = Directory.GetFiles(moduleFolder);

			if(files.Length == 0)
			{
				Logger.Info("No modules found.");
				return;
			}

			foreach(string s in files)
			{
				if(s.EndsWith(".dll")) //there should only be .dll files in the folder but just in case
					ModuleHandler.LoadModule(s, client);
			}
		}
	}
}

