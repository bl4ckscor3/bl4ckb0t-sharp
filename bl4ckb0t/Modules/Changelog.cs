using bl4ckb0t.ModuleAPI;
using System.Collections.Generic;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class Changelog : BaseModule
	{
		public static Dictionary<string, List<string>> versions = new Dictionary<string, List<string>>();

		public Changelog(string name) : base(name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new ChangelogCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.changelog_usage1, Resources.changelog_usage2 };
		}
	}
}
