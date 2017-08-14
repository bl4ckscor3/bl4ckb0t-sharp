using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp.Events;

namespace bl4ckb0t.Modules.ChatReactions
{
	public class ChatReactions : BaseModule
	{
		public ChatReactions(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			client.ChannelMessageRecieved += Handle;
		}

		public override void OnDisable(Bot client)
		{
			client.ChannelMessageRecieved -= Handle;
		}

		public override string[] Usage()
		{
			return new string[] { Utilities.Transform(Resources.usage1, Resources.trigger), Resources.usage2, Resources.usage3 };
		}

		/// <summary>
		/// Handles the action being sent via channel
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		public void Handle(object sender, PrivateMessageEventArgs e)
		{
			if(Utilities.EqualsIgnoreCase(e.PrivateMessage.Message.ToLower().Split(' ')[0], "re"))
				Utilities.SendMessage(e.PrivateMessage.Source, $"wb, {e.PrivateMessage.User.Nick}");
			else if(e.PrivateMessage.IsAction && e.PrivateMessage.Message.Equals("shrugs"))
				Utilities.SendMessage(e.PrivateMessage.Source, "¯\\_(ツ)_/¯");
			else if(Utilities.Matches(e.PrivateMessage.Message.ToLower(), Resources.regex))
				Utilities.SendMessage(e.PrivateMessage.Source, Resources.reply);
		}
	}
}
