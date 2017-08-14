using bl4ckb0t.BotInformation;
using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class RestartCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length == 0)
			{
				Utilities.Client().Quit("Bye");
				Lists.ClearLists();
				ModuleHandler.modules.Clear();
				Logger.Info("Creating new bot");
				Core.CreateBot(Utilities.Wip());
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "restart" };
		}

		public override string Syntax()
		{
			return Resources.shutdown_syntax_restart;
		}
	}
}
