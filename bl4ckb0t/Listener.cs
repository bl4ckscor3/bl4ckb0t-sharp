using bl4ckb0t.BotInformation;
using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Modules;
using bl4ckb0t.Util;
using ChatSharp;
using ChatSharp.Events;
using System;
using System.Threading;

namespace bl4ckb0t
{
	public class Listener
	{
		/// <summary>
		/// Adds all listeners to the events
		/// </summary>
		/// <param name="client">The client to add the listeners to</param>
		public static void Setup(Bot client)
		{
			client.ChannelMessageRecieved += OnMessage;
			client.UserMessageRecieved += OnPrivateMessage;
			client.PrivateMessageRecieved += OnAction;
			client.ConnectionComplete += OnConnect;
		}

		/// <summary>
		/// Gets called when the bot receives a message from a channel
		/// </summary>
		/// <param name="client">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private static void OnMessage(object c, PrivateMessageEventArgs e)
		{
			if(!e.PrivateMessage.Message.StartsWith(Utilities.Prefix()))
				return;

			PrivateMessage privateMessage = e.PrivateMessage;
			string cmdName = privateMessage.Message.Split(' ')[0].Replace(Utilities.Prefix(), "");
			BaseModule executableModule;
			BaseChannelCommand executableCmd;

			foreach(BaseModule m in ModuleHandler.modules)
			{
				if(m.ChannelCmds.Count > 0)
				{
					foreach(BaseChannelCommand cmd in m.ChannelCmds)
					{
						if(cmd.IsValidAlias(cmdName))
						{
							try
							{
								if(m.HasPermission(privateMessage.User.Nick))
								{
									executableModule = m;
									executableCmd = cmd;
									goto execute;
								}
								else
								{
									Utilities.SendMessage(privateMessage.Source, Resources.notAuthorized, privateMessage.User.Nick);
								}
							}
							catch(Exception ex)
							{
								Logger.StackTrace(ex);
								return;
							}
						}
					}
				}
			}

			return;

			execute: {
				if(!executableCmd.Exe(privateMessage, cmdName, Utilities.ToArgs(privateMessage.Message)))
					Help.SendHelp(privateMessage.User.Nick, executableModule);
			}
		}

		/// <summary>
		/// Gets called when the bot receives a message from a user via pm
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private static void OnPrivateMessage(object c, PrivateMessageEventArgs e)
		{
			if(Lists.GetPermissionLevel(e.PrivateMessage.User.Nick) != 3)
				return;

			PrivateMessage privateMessage = e.PrivateMessage;
			string cmdName = privateMessage.Message.Split(' ')[0];

			foreach(BaseModule m in ModuleHandler.modules)
			{
				if(m.PrivateCmds.Count > 0)
				{
					foreach(BasePrivateCommand cmd in m.PrivateCmds)
					{
						if(Utilities.EqualsIgnoreCase(cmdName, cmd.CommandTrigger()))
						{
							try
							{
								if(!cmd.Exe(privateMessage, cmdName, Utilities.ToArgs(privateMessage.Message)))
									Help.SendHelp(privateMessage.User.Nick, m);
							}
							catch(Exception ex)
							{
								Logger.StackTrace(ex);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets called when the bot has received an action (e.g.: * some_random_guy is cool)
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private static void OnAction(object c, PrivateMessageEventArgs e)
		{
			PrivateMessage privateMessage = e.PrivateMessage;

			if(!privateMessage.Message.ToUpper().Contains("ACTION") || Lists.GetPermissionLevel(privateMessage.User.Nick) == 0)
				return;

			if(privateMessage.IsChannelMessage)
				LinkManager.HandleLink(privateMessage.Message, privateMessage.Source, privateMessage.User.Nick);
		}

		/// <summary>
		/// Gets called when the bot has connected to the server
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private static void OnConnect(object c, EventArgs e)
		{
			Logger.Info("Connected");
			Logger.Info("Joining default channels");

			foreach(string s in Lists.GetDefaultChannels())
			{
				((Bot)c).JoinChannel(s);
			}

			Logger.Info("All channels joined");
			Logger.Info("Waiting a bit to fire OnJoined");
			new Timer((status) => {
				foreach(BaseModule m in ModuleHandler.modules)
				{
					m .OnJoined();
				}

				Logger.Info("OnJoined fired to all modules");
			}, null, 2000, Timeout.Infinite);
		}
	}
}
