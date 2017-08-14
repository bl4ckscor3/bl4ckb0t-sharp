using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Join
{
	public class Join : BaseModule
	{
		public Join(string _name) : base(_name) { }

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
		public static bool HandleJoin(PrivateMessage e, string[] args)
		{
			string source = e.Source;

			if(args.Length <= 2)
			{
				if(args[0].Equals("d"))
				{
					Utilities.Client().JoinDefaults();
					return true;
				}

				if(!args[0].StartsWith("#"))
					args[0] = "#" + args[0];

				if(Utilities.HasJoinedChannel(args[0]))
					Utilities.SendMessage(source, Resources.alreadyJoined);
				else
				{
					if(!e.IsChannelMessage)
						Utilities.Client().JoinChannel(args[0], args[1]);
                    else
						Utilities.Client().JoinChannel(args[0]);
				}

				return true;
			}
			else
				return false;
		}
	}
}
