using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bl4ckb0t
{
	class Core
	{
		/// <summary>
		/// Mandatory program arguments:
		///		-name name		Sets the name of the bot to "name".
		///		-prefix !		Sets the command prefix to "!".
		/// 
		/// Optional program arguments:
		///		-wip			Marks this bot as being started as work in progress. This will force a hardcoded name for the bot.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Utilities.AddInformation("name", Utilities.GetArg(args, "-name"));
			Utilities.AddInformation("version", "v7.0");
			Utilities.AddInformation("prefix", Utilities.GetArg(args, "-prefix"));
			Utilities.AddInformation("defaultChannels", new List<string>());
			Utilities.AddInformation("lvl2Users", new List<string>());
			Utilities.AddInformation("lvl3Users", new List<string>());
			Utilities.AddInformation("ignoredUsers", new List<string>());
			Utilities.AddInformation("langs", new Dictionary<string, string>()); //channel,language

			try
			{
				bool wip = args.Contains("-wip");

				Utilities.AddInformation("wip", wip);

				if(wip)
					Utilities.AddInformation("name", "sh4rpb0t");

				Logger.Setup();
				Logger.Info("Setting up bot" + (wip ? " as WIP version" : ""));
				CreateBot();
			}
			catch(Exception e)
			{
				Logger.StackTrace(e);
				Console.ReadKey();
			}
		}

		/// <summary>
		/// Creates the bot and starts it
		/// </summary>
		public static void CreateBot()
		{
			Bot client = new Bot("irc.esper.net:6697", new IrcUser(Utilities.Name(), Utilities.Name()), true);

			Logger.Info("Created client");			
			Logger.Info("Fetching external information");
			Startup.Fetch();
			Logger.Info("Done fetching external information");
			Logger.Info("Setting up listeners");
			Logger.AddListeners(client);
			Listener.Setup(client);
			Logger.Info("Added logging listeners");
			Logger.Info("Loading private modules");
			Startup.InitPrivateModules(client);
			ModuleHandler.FinishLoadingPrivateModules();
			Logger.Info("Loading public modules");
			Startup.InitPublicModules(client);
			Logger.Info("Firing OnFinish() to each module");

			foreach(BaseModule m in ModuleHandler.modules)
			{
				m.OnFinish();
			}

			Logger.Info("All modules loaded");
			Utilities.AddInformation("client", client);
			Logger.Info("Starting bot");
			client.ConnectAsync();

			while(true){;}
		}
	}
}
