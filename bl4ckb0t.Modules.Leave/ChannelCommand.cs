using bl4ckb0t.ModuleAPI;
using ChatSharp;

namespace bl4ckb0t.Modules.Leave
{
	public class ChannelCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			return Leave.HandleLeave(e, args);
		}

		public override string[] Aliases()
		{
			return new string[] { "leave" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
