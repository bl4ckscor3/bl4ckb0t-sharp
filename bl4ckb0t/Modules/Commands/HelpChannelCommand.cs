using bl4ckb0t.ModuleAPI;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class HelpChannelCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Help.SendHelp(e.User.Nick, args);
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "help" };
		}

		public override string Syntax()
		{
			return Resources.help_syntax;
		}
	}
}
