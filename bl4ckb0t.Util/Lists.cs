using bl4ckb0t.Logging;
using bl4ckb0t.Util;
using System.Collections.Generic;

namespace bl4ckb0t.BotInformation
{
	public class Lists
	{
		/// <summary>
		/// Adds a channel as a default of the bot
		/// </summary>
		/// <param name="c">The channel to add</param>
		public static void AddDefaultChannel(string c)
		{
			Utilities.GetInformationAsStringlist("defaultChannels").Add(c);
			Logger.Info("Added " + c + " to channel list");
		}

		/// <summary>
		/// Sets the permission of a user to level 2
		/// </summary>
		/// <param name="u">The user to give the permission to</param>
		public static void AddLvl2User(string u)
		{
			Utilities.GetInformationAsStringlist("lvl2Users").Add(u.ToLower());
			Logger.Info("Set permission level of " + u + " to 2");
		}

		/// <summary>
		/// Sets the permission of a user to level 3
		/// </summary>
		/// <param name="u">The user to give the permission to</param>
		public static void AddLvl3User(string u)
		{
			Utilities.GetInformationAsStringlist("lvl3Users").Add(u.ToLower());
			Logger.Info("Set permission level of " + u + " to 3");
		}

		/// <summary>
		/// Makes the bot ignore a user. It will not react to any message from them
		/// </summary>
		/// <param name="u">The user to ignore</param>
		public static void AddIgnoredUser(string u)
		{
			Utilities.GetInformationAsStringlist("ignoredUsers").Add(u.ToLower());
			Logger.Info("Ignoring " + u + " from now on");
		}

		/// <summary>
		/// Gets the default channels of the bot
		/// </summary>
		/// <returns>The default channels of the bot</returns>
		public static List<string> GetDefaultChannels()
		{
			return Utilities.GetInformationAsStringlist("defaultChannels");
		}

		/// <summary>
		/// Gets all users with the permission level of 2
		/// </summary>
		/// <returns>All users with the permission level of 2</returns>
		public static List<string> GetLvl2Users()
		{
			return Utilities.GetInformationAsStringlist("lvl2Users");
		}
		
		/// <summary>
		/// Gets all users with the permission level of 3
		/// </summary>
		/// <returns>All users with the permission level of 3</returns>
		public static List<string> GetLvl3Users()
		{
			return Utilities.GetInformationAsStringlist("lvl3Users");
		}

		/// <summary>
		/// Gets all users this bot ignores
		/// </summary>
		/// <returns>All users this bot ignores</returns>
		public static List<string> GetIgnoredUsers()
		{
			return Utilities.GetInformationAsStringlist("ignoredUsers");
		}

		/// <summary>
		/// Gets the permission level of a user
		/// </summary>
		/// <param name="u">The user to get the permission level of</param>
		/// <returns>0 if the user is ignored by the bot, 1 if they have default permission, 2 if they have slightly elevated permissions and 3 if they have full control over the bot</returns>
		public static int GetPermissionLevel(string u)
		{
			if(Utilities.GetInformationAsStringlist("ignoredUsers").Contains(u))
				return 0;
			else if(Utilities.GetInformationAsStringlist("lvl2Users").Contains(u))
				return 2;
			else if(Utilities.GetInformationAsStringlist("lvl3Users").Contains(u))
				return 3;
			else
				return 1;
		}

		/// <summary>
		/// Clears all lists regarding default channels and user permissions
		/// </summary>
		public static void ClearLists()
		{
			Utilities.GetInformationAsStringlist("defaultChannels").Clear();
			Utilities.GetInformationAsStringlist("lvl2Users").Clear();
			Utilities.GetInformationAsStringlist("lvl3Users").Clear();
			Utilities.GetInformationAsStringlist("ignoredUsers").Clear();
			Logger.Info("Cleared all lists");
		}
	}
}
