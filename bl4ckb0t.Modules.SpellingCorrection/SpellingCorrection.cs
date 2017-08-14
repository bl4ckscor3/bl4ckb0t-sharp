using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System.Collections.Generic;

namespace bl4ckb0t.Modules.SpellingCorrection
{
	public class SpellingCorrection : BaseModule
	{
		public static readonly Dictionary<string, List<string>> storage = new Dictionary<string, List<string>>(); //<channel, <user#message>>

		public SpellingCorrection(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());
			client.ChannelMessageRecieved += SpellingCorrector.Handle;
		}

		public override void OnDisable(Bot client)
		{
			client.ChannelMessageRecieved -= SpellingCorrector.Handle;
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3, Resources.usage4, Resources.usage5 };
		}
	}
}
