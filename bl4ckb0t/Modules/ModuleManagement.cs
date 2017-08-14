using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class ModuleManagement : BaseModule
	{
		public ModuleManagement(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new ModuleManagementCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.moduleManagement_usage1, Resources.moduleManagement_usage2, Resources.moduleManagement_usage4, Resources.moduleManagement_usage5 };
		}

		public override int RequiredPermissionLevel()
		{
			return 3;
		}
	}
}
