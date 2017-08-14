using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Identify
{
	public class Command : BasePrivateCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Utilities.Client().SendRawMessage("NICKSERV IDENTIFY " + Passwords.nickserv);
			return true;
		}

		public override string CommandTrigger()
		{
			return "identify";
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
