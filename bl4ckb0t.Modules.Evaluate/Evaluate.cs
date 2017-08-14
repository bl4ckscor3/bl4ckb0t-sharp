using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.Evaluate
{
	public class Evaluate : BaseModule
	{
		public Evaluate(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());
		}

		public override string[] Usage()
		{
			return new string[] { };
		}
	}
}
