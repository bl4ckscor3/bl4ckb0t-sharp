using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System;

namespace bl4ckb0t.Modules
{
	public class StopCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length == 0)
			{
				Utilities.Client().Quit("Bye");
				Logger.Info("Bot stopped!");
				Logger.Disable();
				Environment.Exit(0);
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "stop" };
		}

		public override string Syntax()
		{
			return Resources.shutdown_syntax_stop;
		}
	}
}
