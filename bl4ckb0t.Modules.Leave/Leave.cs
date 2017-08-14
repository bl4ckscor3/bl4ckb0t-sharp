using bl4ckb0t.BotInformation;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Leave
{
	public class Leave : BaseModule
	{
		public Leave(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new ChannelCommand());
			RegisterPrivateCommand(new PrivateCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3 };
		}

		public override int RequiredPermissionLevel()
		{
			return 3;
		}

		/// <summary>
		/// Handles both the private and channel command
		/// </summary>
		/// <param name="e">The class containing info about the event calling this method</param>
		/// <param name="args">The arguments of the command sent by a user</param>
		/// <returns></returns>
		public static bool HandleLeave(PrivateMessage e, string[] args)
		{
			string channel = e.Source;
			Bot bot = Utilities.Client();

			if(args.Length == 1)
			{
				if(args[0].Equals("d"))
				{
					foreach(string s in Lists.GetDefaultChannels())
					{
						bot.LeaveChannel(s);
					}

					return true;
				}

				if(!args[0].StartsWith("#"))
					args[0] = $"#{args[0]}";

				if(Utilities.HasJoinedChannel(args[0]))
					bot.LeaveChannel(args[0]);
				else
					Utilities.SendMessage(channel, Resources.notJoined);
			}
			else if(args.Length == 0)
				bot.LeaveChannel(channel);
			else
				return false;

			return true;
		}
	}
}
