using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class SourceCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Utilities.SendMessage(e.Source, "https://github.com/bl4ckscor3/bl4ckb0t");
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "source", "src" };
		}

		public override string Syntax()
		{
			return Resources.source_syntax;
		}
	}
}
