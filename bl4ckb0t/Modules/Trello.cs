using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class Trello : BaseModule
	{
		public Trello(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new TrelloCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.trello_usage };
		}
	}
}
