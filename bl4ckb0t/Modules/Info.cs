using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System.Reflection;

namespace bl4ckb0t.Modules
{
	public class Info : BaseModule
	{
		public Info(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new InfoCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.info_usage };
		}
	}
}
