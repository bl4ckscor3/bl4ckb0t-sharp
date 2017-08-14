using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Socials
{
	public class YouTube : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 1)
			{
				try
				{
					WebWrapper.NewNSoup($"http://www.youtube.com/{args[0]}");
					Utilities.SendMessage(channel, $"http://www.youtube.com/{args[0]}");
				}
				catch(NSoup.HttpStatusException ex)
				{
					if(ex.StatusCode == 404)
						Utilities.SendMessage(channel, Resources.error);
				}

				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "yt", "youtube" };
		}

		public override string Syntax()
		{
			return Resources.yt_syntax;
		}
	}
}
