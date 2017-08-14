using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.Select
{
	public class Select : BaseModule
	{
		public Select(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}

		public override string Notes()
		{
			return Resources.notes;
		}
	}
}
