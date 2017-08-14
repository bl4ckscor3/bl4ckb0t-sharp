using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.SendMessage
{
	public class Command : BasePrivateCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length > 1)
			{
				Utilities.SendMessage(args[0], Utilities.ConcatAt(args, 1));
				return true;
			}
			else
				return false;
		}

		public override string CommandTrigger()
		{
			return "send";
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
