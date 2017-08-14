using bl4ckb0t.ModuleAPI;
using ChatSharp;

namespace bl4ckb0t.Modules.Join
{
	public class ChannelCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			return Join.HandleJoin(e, args);
		}

		public override string[] Aliases()
		{
			return new string[] { "join" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
