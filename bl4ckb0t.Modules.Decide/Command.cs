using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System;

namespace bl4ckb0t.Modules.Decide
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length >= 1 && e.Message.Trim().EndsWith("?"))
			{
				double decision = new Random().NextDouble();

				if(decision < 0.5D)
					Utilities.SendMessage(channel, Resources.replyNo);
				else if(decision <= 1.0D)
					Utilities.SendMessage(channel, Resources.replyYes);
				else
					Utilities.SendMessage(channel, "Something went wrong that shouldn't: {0}", decision);

				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "decide" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
