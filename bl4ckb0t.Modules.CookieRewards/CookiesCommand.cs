using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System.Collections.Generic;
using System.IO;

namespace bl4ckb0t.Modules.CookieRewards
{
	public class CookiesCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string nick = e.User.Nick;
			string channel = e.Source;

			if(args.Length > 1)
				return false;
			else if(args.Length == 0)
				args = new string[] { nick };

			List<string> contents = Utilities.ReadLines(new StreamReader(CookieRewards.path), true);

			foreach(string s in contents)
			{
				string name = s.Split('#')[0];

				if(name.Equals(args[0]))
				{
					int amount = int.Parse(s.Split('#')[1]);
					int given = int.Parse(s.Split('#')[2]);
					int received = int.Parse(s.Split('#')[3]);

					Utilities.SendStarMsg(channel,
						Utilities.Transform(Resources.user, name),
						Utilities.Transform(Resources.amount, amount),
						Utilities.Transform(Resources.given, given),
						Utilities.Transform(Resources.received, received));
					return true;
				}
			}

			contents.Add($"{args[0]}#20#0#0");
			Utilities.WriteLines(new StreamWriter(CookieRewards.path, false), contents, true);
			Utilities.SendStarMsg(channel,
				Utilities.Transform(Resources.user, args[0]),
				Utilities.Transform(Resources.amount, 20),
				Utilities.Transform(Resources.given, 0),
				Utilities.Transform(Resources.received, 0));
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "cookies" };
		}

		public override string Syntax()
		{
			return Resources.syntax_cookies;
		}
	}
}
