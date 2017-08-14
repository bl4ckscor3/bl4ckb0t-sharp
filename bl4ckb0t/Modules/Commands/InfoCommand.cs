using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class InfoCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Utilities.SendStarMsg(e.Source,
				Utilities.Transform(Resources.info_version, Utilities.Version()),
				Utilities.Transform(Resources.info_uptime, Utilities.UnixToFormat(Utilities.Uptime(), "{0}:{1}:{2}:{3}")),
				Utilities.Transform(Resources.info_buildDate, Utilities.BuildDate().ToString(),
				Utilities.Transform(Resources.info_dotNetVersion, "4.5.2"),
				Utilities.Transform(Resources.info_author)));
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "info", "about" };
		}

		public override string Syntax()
		{
			return Resources.info_syntax;
		}
	}
}
