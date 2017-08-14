using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Logging;
using bl4ckb0t.Util;
using ChatSharp;
using NSoup.Nodes;
using System;

namespace bl4ckb0t.Modules.Thumbnail
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 1)
			{
				string idRegex = "[A-Za-z0-9-_]{11}";
				bool idMatch = Utilities.Matches(args[0], idRegex);

				//"https://" or "http://" or nothing, followed by www.youtube.com/watch?v= followed by 11 interchangeable upper or lower cased letters, or numbers or - or _, OR just tje followed by part
				if(Utilities.Matches(args[0], $"(https:\\/\\/|http:\\/\\/|)(www\\.youtube\\.com\\/watch\\?v=|youtu\\.be\\/)({idRegex})(&[A-Za-z0-9=&]*|)") || idMatch)
				{
					string link = idMatch ? $"http://www.youtube.com/watch?v={args[0]}" : args[0];

					if(link.StartsWith("www"))
						link = $"https://{link}";

					try
					{
						foreach(Element el in WebWrapper.NewNSoup(link).Select("head > meta").ToArray())
						{
							if(el.Attr("property").Equals("og:image"))
								Utilities.SendMessage(channel, el.Attr("content"));
						}
					}
					catch(Exception ex)
					{
						Logger.StackTrace(ex);
						Utilities.SendMessage(channel, Resources.error);
					}
				}
				else
					Utilities.SendMessage(channel, Resources.noVideo);

				return true;
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "thumbnail", "thumb", "tn" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
