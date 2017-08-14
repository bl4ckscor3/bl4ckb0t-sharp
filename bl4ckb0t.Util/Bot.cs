using bl4ckb0t.BotInformation;
using bl4ckb0t.Logging;
using ChatSharp;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace bl4ckb0t.Util
{
	public class Bot : IrcClient
	{
		public Bot(string serverAddress, IrcUser user, bool useSSL = false) : base(serverAddress, user, useSSL) { }
		
		/// <summary>
		/// Kicks a user from a channel
		/// </summary>
		/// <param name="user">The user to kick</param>
		/// <param name="channel">The channel to kick the user from</param>
		/// <param name="reason">The reason for the kick</param>
		public void Kick(string user, string channel, string reason = null)
		{
			if(reason == null)
				reason = user;

			SendRawMessage("KICK {0} {1} :{2}", channel, user, reason);
			Logger.Info("Kicked " + user + " from " + channel + ": \"" + reason + "\"");
		}

		/// <summary>
		/// Lets the bot quit the server
		/// </summary>
		/// <param name="reason">The reason to quit</param>
		public new void Quit(string reason = null) //new because IrcClient has a quit method
		{
			base.Quit(reason); //base because the call for Quit in IrcClient is needed and not a recursive call
			Logger.Severe("BOT LEFT THE SERVER");
		}

		/// <summary>
		/// Joins a channel
		/// </summary>
		/// <param name="channel">The channel to join</param>
		/// <param name="password">The password of the channel, if it has one</param>
		public void JoinChannel(string channel, string password = null)
		{
			SendRawMessage("JOIN " + channel + (password == null ? "" : " " + password));
			Logger.Info("Joined " + channel);
		}

		/// <summary>
		/// Leaves a channel
		/// </summary>
		/// <param name="channel">The channel to leave</param>
		/// <param name="reason">The reason to leave</param>
		public void LeaveChannel(string channel, string reason = null)
		{
			SendRawMessage("PART " + channel + " :" + reason);
			Logger.Info("Left " + channel + (reason == null ? "" : " for \"" + reason + "\""));
		}

		/// <summary>
		/// Sends an action to a channel or user (e.g.: * some_random_guy is cool)
		/// </summary>
		/// <param name="target">The channel or user to receive the action</param>
		/// <param name="content">The message of the action</param>
		public void Action(string target, string content)
		{
			SendAction(content, target);
			Logger.Action(target, Utilities.Name(), content);
		}

		/// <summary>
		/// Joins all default channels
		/// </summary>
		public void JoinDefaults()
		{
			List<string> channelsToJoin = Lists.GetDefaultChannels();
			Type t = new Passwords().GetType();

			foreach(string c in channelsToJoin)
			{
				foreach(PropertyInfo info in t.GetProperties())
				{
					if(c.EndsWith(info.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						JoinChannel(c, (string)info.GetValue(t));
						goto outerCont;
					}
				}

				JoinChannel(c); //this should be skipped if the channel has a password and has been joined
				outerCont: { }
			}
		}
	}
}
