using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.Identify
{
	public class Identify : BaseModule
	{
		public Identify(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterPrivateCommand(new Command());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}
	}
}
