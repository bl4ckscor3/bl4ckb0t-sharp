using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System.Collections.Generic;

namespace bl4ckb0t.Modules.ListChans
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			ChannelCollection channels = Utilities.Client().User.Channels;
			List<string> secretChannels = new List<string>();
			string result = "";
			string nick = e.User.Nick;

			foreach(IrcChannel c in channels)
			{
				if(c.Mode.Contains("s"))
					secretChannels.Add(c.Name);
				else
					result += c.Name + " | ";
			}

			if(result.Contains(" | "))
				result = result.Substring(0, result.Length - result.LastIndexOf(" | "));

			if(secretChannels.Count > 0)
				result += string.Format(Resources.secret, secretChannels.Count);

			Utilities.SendMessage(e.Source, Resources.list, result);

			if(Utilities.IsLvl3User(nick))
			{
				Utilities.SendMessage(nick, Resources.showSecret);
				
				foreach(string s in secretChannels)
				{
					Utilities.SendMessage(nick, s);
				}
			}

			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "listchans", "lc" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
