using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class Shutdown : BaseModule
	{
		public Shutdown(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new StopCommand());
			RegisterChannelCommand(new RestartCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.shutdown_usage1, Resources.shutdown_usage2 };
		}

		public override int RequiredPermissionLevel()
		{
			return 3;
		}
	}
}
