using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using NSoup.Nodes;

namespace bl4ckb0t.Modules.Distance
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length == 2)
			{
				Document doc = WebWrapper.NewNSoup($"http://www.distance.to/{args[0]}/{args[1]}");
				string directMI = doc.Select("#airline")[0].Text().Replace(",", "");
				string directKM = string.Format("{0:N2}", double.Parse(directMI.Split(' ')[0]) * 1.621371D).Replace(",", "");

				Utilities.SendStarMsg(e.Source,
					$"{Colors.NORMAL}{directKM}km{Colors.ITALICS} ({directMI})",
					Utilities.Transform(Resources.credit, $"{Colors.NORMAL}http://www.distance.to/"));
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "distance", "dist" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
