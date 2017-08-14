using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Unshorten
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 1)
			{
				string output = WebWrapper.NewNSoup($"http://api.unshorten.it?shortURL={args[0]}&apiKey={Passwords.unshortenapikey}").Text();

				Logging.Logger.Debug(output);

				if(output.Equals("error (0)"))
					Utilities.SendMessage(channel, Resources.invalidURL);
				else if(output.Equals("error (3)"))
					Utilities.SendMessage(channel, Resources.couldntUnshort);
				else
					Utilities.SendMessage(channel, output);

				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "unshorten", "longurl" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
