using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Select
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length == 0)
				return false;

			string[] options = e.Message.Substring(8).Split(',');

			Utilities.SendMessage(e.Source, options[new System.Random().Next(options.Length)].Trim());
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "select", "choose" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
