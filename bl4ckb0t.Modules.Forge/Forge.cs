using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.Forge
{
	public class Forge : BaseModule
	{
		public Forge(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3, Resources.usage4, Resources.usage5};
		}
	}
}
