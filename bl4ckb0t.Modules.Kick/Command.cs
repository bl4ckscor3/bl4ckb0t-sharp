using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Kick
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;
			string nick = e.User.Nick;

			if(args.Length >= 1)
			{
				if(Utilities.EqualsIgnoreCase(args[0], Utilities.Name()))
				{
					Utilities.SendMessage(channel, Resources.cannotKick);
					return true;
				}

				if(Utilities.IsLvl3User(args[0]))
				{
					Utilities.SendMessage(channel, Resources.cannotKick);
					return true;
				}

				Utilities.Client().Kick(args[0], channel, Utilities.ConcatAt(args, (args.Length == 1 ? 0 : 1)));
                return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "kick" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
