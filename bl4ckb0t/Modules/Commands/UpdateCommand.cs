using bl4ckb0t.BotInformation;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class UpdateCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Lists.ClearLists();
			Changelog.versions.Clear();
			Startup.Fetch();

			foreach(BaseModule m in ModuleHandler.modules)
			{
				m.OnUpdate();
			}

			Utilities.SendMessage(e.Source, Resources.update_success);
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "update" };
		}

		public override string Syntax()
		{
			return Resources.update_syntax;
		}
	}
}
