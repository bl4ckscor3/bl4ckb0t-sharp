using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System.Text;

namespace bl4ckb0t.Modules.SpellingCorrection
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string lastMessage = "";
			string channel = e.Source;
			string user = e.User.Nick;
			
			foreach(string s in SpellingCorrection.storage[channel])
			{
				if(s.Split('#')[0].Equals(user))
					lastMessage = s.Split('#')[1];
			}

			if(lastMessage.Equals(""))
				return true;

			StringBuilder builder = new StringBuilder();

			if(args.Length == 0)
			{
				builder.Append(lastMessage);

				for(int i = 0; i < builder.Length; i++)
				{
					char c = builder[i];

					if(char.IsLower(c))
						builder[i] = char.ToUpper(c);
					else if(char.IsUpper(c))
						builder[i] = char.ToLower(c);
				}
			}
			else
			{
				if(args[0].Equals("up"))
					builder.Append(lastMessage.ToUpper());
				else if(args[0].Equals("low"))
					builder.Append(lastMessage.ToLower());
				else
					return false;
			}

			Utilities.SendMessage(channel, builder.ToString());
			SpellingCorrector.UpdateLastMessage(channel, user, builder.ToString());
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "caps" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
