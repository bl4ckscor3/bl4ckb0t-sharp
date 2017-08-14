using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.Socials
{
	public class Socials : BaseModule
	{
		public Socials(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Twitch());
			RegisterChannelCommand(new Twitter());
			RegisterChannelCommand(new YouTube());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3 };
		}
	}
}
