using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using NSoup;
using NSoup.Nodes;
using System;

namespace bl4ckb0t.Modules.Forge
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 3)
			{
				Document doc = null;

				try
				{
					doc = WebWrapper.NewNSoup($"http://files.minecraftforge.net/maven/net/minecraftforge/forge/index_{args[0]}.html");	
				}
				catch(HttpStatusException ex)
				{
					if(ex.StatusCode == 404)
						Utilities.SendMessage(channel, Resources.incorrectMcVersion);
					else
						Utilities.SendMessage(channel, Resources.unexpectedError, ex.StatusCode);

					return true;
				}

				string msg = ""; //message getting sent to the user
				string version = ""; //requested version
				string date = ""; //build date of version
				Element link = null;

				if(Utilities.EqualsIgnoreCase(args[1], "latest"))
					link = doc.Select(".download")[0];
				else if(Utilities.EqualsIgnoreCase(args[1], "rec") || Utilities.EqualsIgnoreCase(args[1], "recommended"))
					link = doc.Select(".download")[1];
				else
					return false;

				try
				{
					version = link.Select("small").Html();
					date = version.Split('\n')[1].Replace("<!-- b", "B").Replace("-->", "").Trim();
					version = version.Split('\n')[0].Replace(" ", "").Trim();
				}
				catch(Exception)
				{
					Utilities.SendMessage(channel, Resources.versionNotFound);
					return true;
				}

				msg = $"http://files.minecraftforge.net/maven/net/minecraftforge/forge/{version}/forge-{version}-";

				switch(args[2])
				{
					case "version":
						msg = Colors.BOLD + version + Colors.NORMAL + $"({date})";
						break;
					case "changelog":
						msg += "changelog.txt";
						break;
					case "dlmain":
						msg += "installer.jar";
						break;
					case "dlmdk":
						msg += "mdk.zip";
						break;
					default:
						return false;
				}

				Utilities.SendMessage(channel, msg);
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "forge" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
