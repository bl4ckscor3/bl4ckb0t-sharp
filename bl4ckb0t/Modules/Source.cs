using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class Source : BaseModule
	{
		public Source(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new SourceCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.source_usage };
		}
	}
}
