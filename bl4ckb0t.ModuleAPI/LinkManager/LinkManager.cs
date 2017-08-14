using bl4ckb0t.Util;
using ChatSharp.Events;
using System.Collections.Generic;

namespace bl4ckb0t.ModuleAPI //it's a module, but for convenience purposes it's placed in this namespace
{
	public class LinkManager : BaseModule
	{
		private static readonly List<ILinkAction> linkActions = new List<ILinkAction>();

		public LinkManager(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			client.ChannelMessageRecieved += CallHandleLink; //i love this :D
		}

		public override void OnDisable(Bot client)
		{
			client.ChannelMessageRecieved -= CallHandleLink;
		}

		public override string[] Usage()
		{
			return new string[] { Resources.linkmanager_usage1, Resources.linkmanager_usage2 };
		}

		/// <summary>
		/// Registers a LinkAction to the LinkManager
		/// Make sure to call removeLinkAction() once the LinkAction is no longer needed (for instance when the module gets disabled)
		/// </summary>
		/// <param name="la">The LinkAction to register</param>
		public static void RegisterLinkAction(ILinkAction la)
		{
			linkActions.Add(la);
			linkActions.Sort((la1, la2) => {
				if(la1.Priority() > la2.Priority())
					return -1;
				else if(la1.Priority() < la2.Priority())
					return 1;
				else
					return 0;
			});
		}

		/// <summary>
		/// Removes a LinkAction from this LinkManager
		/// </summary>
		/// <param name="la">The LinkAction to remove</param>
		public static void RemoveLinkAction(ILinkAction la)
		{
			linkActions.Remove(la);
		}

		/// <summary>
		/// Calls the method to handle links
		/// </summary>
		/// <param name="c">The Irc Client</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private void CallHandleLink(object c, PrivateMessageEventArgs e)
		{
			HandleLink(e.PrivateMessage.Message, e.PrivateMessage.Source, e.PrivateMessage.User.Nick);
		}

		/// <summary>
		/// Takes actions when specific links are sent
		/// </summary>
		/// <param name="message">The message containing links</param>
		/// <param name="channel">The channel the message got sent in</param>
		/// <param name="user">The user the message got sent by</param>
		public static void HandleLink(string message, string channel, string user)
		{
			if(message.StartsWith(Utilities.Prefix()))
				return;

			foreach(string s1 in message.Split(' '))
			{
				string s = Colors.RemoveColorsAndFormatting(s1);

				if(s.Contains("www.") || s.Contains("http://") || s.Contains("https://"))
				{
					foreach(ILinkAction la in linkActions)
					{
						if(la.IsValid(s))
						{
							if(s.StartsWith("www"))
								s = $"http://{s}";

							la.Handle(channel, user, s);
							goto outerCont;
						}
					}
				}

				outerCont: { }
			}
		}
	}
}
