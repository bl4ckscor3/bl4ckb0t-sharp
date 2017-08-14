using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.SendMessage
{
	public class SendMessage : BaseModule
	{
		public SendMessage(string _name) : base(_name) { }

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
