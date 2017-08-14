using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp.Events;

namespace bl4ckb0t.Modules.Ping
{
	public class Ping : BaseModule
	{
		public Ping(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			client.ChannelMessageRecieved += HandleChannel;
			client.UserMessageRecieved += HandlePrivate;
		}

		public override void OnDisable(Bot client)
		{
			client.ChannelMessageRecieved -= HandleChannel;
			client.UserMessageRecieved -= HandlePrivate;
		}

		/// <summary>
		/// Handles messages "-ping" and "-pong" sent via a channel message
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private void HandleChannel(object c, PrivateMessageEventArgs e)
		{
			if(e.PrivateMessage.Message.StartsWith(Utilities.Prefix() + "ping"))
				Utilities.SendMessage(e.PrivateMessage.Source, Resources.reply1);
			else if(e.PrivateMessage.Message.StartsWith(Utilities.Prefix() + "pong"))
				Utilities.SendMessage(e.PrivateMessage.Source, Resources.reply2);
		}

		/// <summary>
		/// Handles messages "ping" and "pong" sent via private message
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private void HandlePrivate(object c, PrivateMessageEventArgs e)
		{
			if(e.PrivateMessage.Message.StartsWith("ping"))
				Utilities.SendMessage(e.PrivateMessage.Source, Resources.reply1);
			else if(e.PrivateMessage.Message.StartsWith("pong"))
				Utilities.SendMessage(e.PrivateMessage.Source, Resources.reply2);
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3};
		}
	}
}
