using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class ChangelogCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length >= 1)
			{
				if(!Changelog.versions.ContainsKey(args[0]))
				{
					Utilities.SendMessage(channel, Resources.changelog_notFound);
					return true;
				}

				foreach(string s in Changelog.versions[args[0]])
				{
					Utilities.SendMessage(channel, s);
				}

				return true;
			}
			else if(!Utilities.Wip())
			{
				foreach(string s in Changelog.versions[Utilities.Version()])
				{
					Utilities.SendMessage(channel, s);
				}

				return true;
			}

			return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "changelog", "cl" };
		}

		public override string Syntax()
		{
			return Resources.changelog_syntax;
		}
	}
}
