using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules
{
	public class TrelloCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Utilities.SendMessage(e.Source, "https://trello.com/b/039j1jFa/bl4ckb0t");
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "trello", "tr" };
		}

		public override string Syntax()
		{
			return Resources.trello_syntax;
		}
	}
}
