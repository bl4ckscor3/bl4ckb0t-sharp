using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System.IO;
using System.Net;

namespace bl4ckb0t.Modules.IPLocation
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 1)
			{
				WebClient client = WebWrapper.NewClient();
				StreamReader reader = new StreamReader(client.OpenRead($"http://freegeoip.net/xml/{args[0]}"));
				string s = "";
				string country = "";
				string region = "";
				string city = "";

				if(reader.ReadLine().Contains("404"))
				{
					Utilities.SendMessage(channel, Resources.http404);
					return true;
				}

				while((s = reader.ReadLine()) != null)
				{
					if(s.Contains("CountryName"))
						country = Utilities.ExtractXML(s);
					else if(s.Contains("RegionName"))
						region = Utilities.ExtractXML(s);
					else if(s.Contains("City"))
						city = Utilities.ExtractXML(s);
				}

				Utilities.SendStarMsg(channel,
					Utilities.Transform(Resources.country, country),
					Utilities.Transform(Resources.region, region),
					Utilities.Transform(Resources.city, city),
					Utilities.Transform(Resources.credit, "http://freegeoip.net"));
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "iplocation", "iploc", "ip", "loc" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
