using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.ChangeNick
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 1)
			{
				if(args[0].Equals(Utilities.Client().User.Nick))
				{
					Utilities.SendMessage(channel, Resources.sameNick);
					return true;
				}
				else if(Utilities.EqualsIgnoreCase(args[0], "d")) //default
					Utilities.Client().Nick(Utilities.Name());
				else
					Utilities.Client().Nick(args[0]);

				ChangeNick.lastChannel = channel;
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "changenick", "cn", "nick" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
