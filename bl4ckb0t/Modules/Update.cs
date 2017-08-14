using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class Update : BaseModule
	{
		public Update(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new UpdateCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.update_usage };
		}
	}
}
