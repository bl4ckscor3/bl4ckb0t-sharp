using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Leet
{
	public class UnleetCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length >= 1)
			{
				char[] chars = e.Message.Substring(8).ToCharArray();
				string result = "";

				foreach(char c in chars)
				{
					result += Leet.ToNormal(c);
				}

				Utilities.SendMessage(e.Source, result);
				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "unleet" };
		}

		public override string Syntax()
		{
			return Resources.syntax2;
		}
	}
}
