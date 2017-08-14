using bl4ckb0t.Util;
using System.Collections.Generic;
using ChatSharp.Events;
using System;

namespace bl4ckb0t.Modules.SpellingCorrection
{
	public class SpellingCorrector
	{
		private static bool corrected = false; //needed to check if the message should be added to the array or not

		/// <summary>
		/// Handles the message being sent via channel
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		public static void Handle(object c, PrivateMessageEventArgs e)
		{
			if(e.PrivateMessage.Message.StartsWith(Utilities.Prefix()))
				return;
			
			CheckForSpellingCorrection(e.PrivateMessage.Source, e.PrivateMessage.User.Nick, e.PrivateMessage.Message);

			//making sure the above messages dont get added as a latest message
			if(!corrected)
				UpdateLastMessage(e.PrivateMessage.Source, e.PrivateMessage.User.Nick, e.PrivateMessage.Message);
			else
				corrected = false;
		}

		/// <summary>
		/// Determine if the message contains a syntax to correct a message sent before
		/// 
		/// TO PAST ME: WTF DID YOU DO HERE I DON'T UNDERSTAND ANYTHING
		/// 
		/// </summary>
		/// <param name="channel">The channel the SpellingCorrection got called from</param>
		/// <param name="user">The user who called the SpellingCorrection</param>
		/// <param name="message">The message sent to the channel which triggered the event</param>
		private static void CheckForSpellingCorrection(string channel, string user, string message)
		{
			string[] spaceSplit = message.Split(' ');

			//checking if someone corrects someone else
			if(spaceSplit.Length > 1 && (spaceSplit[0].EndsWith(":") || spaceSplit[0].EndsWith(",")))
			{
				bool colon;

				if(spaceSplit[0].EndsWith(":"))
					colon = true;
				else
					colon = false;

				if(spaceSplit[1].StartsWith("s/"))
				{
					String[] split;
					String newMessage = "";
					int i = 0;

					//actually getting only the s/x/y message if it contains spaces	
					foreach(string s in spaceSplit)
					{
						if(i != 0)
							newMessage += s + " ";

						i++;
					}

					//removing the last character of the string to prevent 2 spaces
					newMessage = newMessage.Substring(0, newMessage.Length - 1);

					if(newMessage.EndsWith("/"))
						newMessage += " ";

					split = newMessage.Split('/');

					if(split.Length == 3 && split[0].Equals("s"))
					{
						CorrectSpelling(channel, user, split, true, colon ? message.Split(':')[0] : message.Split(',')[0]);
						corrected = true;
					}

					return;
				}
			}

			//checking if someone is correcting himself
			if(message.StartsWith("s/"))
			{
				String[] split;

				if(message.EndsWith("/"))
					message += "/"; //a not displayed character to prevent a nullpointer exception

				split = message.Split('/');

				if(split.Length == 3 && split[0].Equals("s"))
				{
					CorrectSpelling(channel, user, split, false, user);
					corrected = true;
				}
			}
		}

		/// <summary>
		/// Prepares and sends the message of the corrected spelling
		/// </summary>
		/// <param name="channel">The channel the SpellingCorrection got called from</param>
		/// <param name="user">The user who called the SpellingCorrection</param>
		/// <param name="split">The message split by slashes</param>
		/// <param name="correctsDifferentUser">Wether or not the issuer corrects their own or another person's spelling</param>
		/// <param name="userToCorrect">The user to correct the spelling of</param>
		private static void CorrectSpelling(string channel, string user, string[] split, bool correctsDifferentUser, string userToCorrect)
		{
			String toReplace = split[1];
			String replaceWith = split[2];

			foreach(string s in SpellingCorrection.storage[channel])
			{
				if(s == null)
					break;

				if(userToCorrect.Equals(s.Split('#')[0]))
				{
					String previousMessage;
					String newMessage;
					String correctedMessage;

					if(toReplace.Equals(""))
					{
						previousMessage = GetLastMessage(userToCorrect, SpellingCorrection.storage[channel]);
						newMessage = replaceWith;
						correctedMessage = Colors.ITALICS + replaceWith;
					}
					else
					{
						previousMessage = GetLastMessage(userToCorrect, SpellingCorrection.storage[channel]);
						newMessage = GetLastMessage(userToCorrect, SpellingCorrection.storage[channel]).Replace(toReplace, replaceWith); //w/o italics
						correctedMessage = GetLastMessage(userToCorrect, SpellingCorrection.storage[channel]).Replace(toReplace, Colors.ITALICS + replaceWith + Colors.NORMAL); //w/ italics
					}

					if(previousMessage.Equals(correctedMessage))
						return;

					if(correctsDifferentUser)
						Utilities.SendMessage(channel, Resources.correctingOther, userToCorrect, user, correctedMessage);
					else
						Utilities.SendMessage(channel, Resources.correctingSelf, userToCorrect, correctedMessage);

					UpdateLastMessage(channel, userToCorrect, newMessage);
				}
			}
		}

		/// <summary>
		/// Updates the last sent message of a user in a channel
		/// </summary>
		/// <param name="channel">The channel to update the message in</param>
		/// <param name="user">The user to update the message of</param>
		/// <param name="message">The message</param>
		public static void UpdateLastMessage(string channel, string username, string msg)
		{
			int i = 0;

			if(!SpellingCorrection.storage.ContainsKey(channel))
				SpellingCorrection.storage[channel] = new List<String>();

			foreach(string s in SpellingCorrection.storage[channel])
			{
				//if the current array position contains no data to replace, stop the loop and add the data
				if(s == null)
					break;

				//checking for the correct array position to potentially replace the latest message with
				if(s.Split('#')[0].Equals(username))
				{
					SpellingCorrection.storage[channel][i] = $"{username}#{msg}";
					return;
				}

				i++;
			}

			SpellingCorrection.storage[channel].Add(username + "#" + msg);
		}

		/// <summary>
		/// Returns the last message from the given user in the given user
		/// </summary>
		/// <param name="user">The name of the user to get the latest message from</param>
		/// <param name="messages">The list containing the latest message</param>
		/// <returns>The latest message from the given user</returns>
		private static string GetLastMessage(string user, List<string> messages)
		{
			foreach(string s in messages)
			{
				if(s.Split('#')[0].Equals(user))
					return s.Split('#')[1];
			}

			return "Will never happen.";
		}
	}
}
