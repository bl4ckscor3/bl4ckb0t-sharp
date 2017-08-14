using bl4ckb0t.ModuleAPI;
using ChatSharp;

namespace bl4ckb0t.Modules.Join
{
	public class PrivateCommand : BasePrivateCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			return Join.HandleJoin(e, args);
		}

		public override string CommandTrigger()
		{
			return "join";
		}

		public override string Syntax()
		{
			return Resources.syntax.Replace("%cmd%", "");
		}
	}
}
