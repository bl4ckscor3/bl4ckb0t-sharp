using System.Linq;

namespace ChatSharp
{
    /// <summary>
    /// Represents an IRC message sent from user-to-user or user-to-channel.
    /// </summary>
    public class PrivateMessage
    {
        internal PrivateMessage(IrcClient client, IrcMessage message, ServerInfo serverInfo, bool action)
        {
            Source = message.Parameters[0];
            Message = message.Parameters[1].Replace("ACTION ", "").Replace("", ""); //there's some weird special character that needs to be removed
			IsAction = action;

            User = client.Users.GetOrAdd(message.Prefix);
            if (serverInfo.ChannelTypes.Any(c => Source.StartsWith(c.ToString())))
                IsChannelMessage = true;
            else
                Source = User.Nick;
        }

        /// <summary>
        /// The user that sent this message.
        /// </summary>
        public IrcUser User { get; set; }
        /// <summary>
        /// The message text.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The source of the message (a nick or a channel name).
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// True if this message was posted to a channel.
        /// </summary>
        public bool IsChannelMessage { get; set; }
		/// <summary>
		/// True if this message is an action (/me)
		/// </summary>
		public bool IsAction { get; set; }
    }
}
