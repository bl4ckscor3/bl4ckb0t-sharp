using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.Kick
{
	public class Kick : BaseModule
	{
		public Kick(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2 };
		}

		public override int RequiredPermissionLevel()
		{
			return 2;
		}
	}
}
