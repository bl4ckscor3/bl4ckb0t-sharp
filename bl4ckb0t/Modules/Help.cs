using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules
{
	public class Help : BaseModule
	{
		public Help(string name) : base(name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new HelpChannelCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.help_usage1, Resources.help_usage2 };
		}

		/// <summary>
		/// Sends help to a user, distinguishes between help about a module and showing all modules + credits
		/// </summary>
		/// <param name="user">The user to send help to</param>
		/// <param name="args">The arguments usually containing what the user wants help about</param>
		public static void SendHelp(string user, string[] args)
		{
			if(args.Length == 0)
			{
				string msg = "";
				int i = 0;

				Utilities.SendMessage(user, Resources.help_genericHelpHeader);

				foreach(BaseModule m in ModuleHandler.modules)
				{
					if(m.HasPermission(user))
					{
						msg += $"{(m.PrivateModule ? Colors.UNDERLINE : "")}{(m.RequiredPermissionLevel() == 3 ? Colors.RED : (m.RequiredPermissionLevel() == 2 ? Colors.GREEN : ""))}{m.Name}{Colors.NORMAL} | ";
						i++;

						if(i % 10 == 0)
						{
							Utilities.SendMessage(user, msg.Trim());
							msg = "";
						}
					}
				}

				Utilities.SendMessage(user, msg.Substring(0, msg.LastIndexOf(" | ")));
				Utilities.SendMessage(user, $"{Colors.BOLD}{Colors.OLIVE}----------------------------------------------------------");
				Utilities.SendMessage(user, Resources.help_moreInfo);
				Utilities.SendMessage(user, Resources.help_creditsHeader);
				Utilities.SendMessage(user, Resources.help_credits1);
				Utilities.SendMessage(user, Resources.help_credits2);
				Utilities.SendMessage(user, Resources.help_credits3);
				Utilities.SendMessage(user, Resources.help_credits4);
			}
			else if(args.Length == 1)
				SendHelp(user, ModuleHandler.GetModuleByName(args[0]));
		}

		/// <summary>
		/// Sends help to a user about a module
		/// </summary>
		/// <param name="user">The user to send help to</param>
		/// <param name="m">The module to send the help about</param>
		public static void SendHelp(string user, BaseModule m)
		{
			if(m.HasPermission(user))
			{
				Utilities.SendMessage(user, Colors.BOLD + Colors.OLIVE + Resources.help_moduleHelpHeader, m.Name);
				Utilities.SendMessage(user, Resources.help_channelCommands);

				if(m.ChannelCmds.Count > 0)
				{
					foreach(BaseChannelCommand c in m.ChannelCmds)
					{
						string result = Utilities.Transform(c.Syntax());

						if(c.Aliases().Length > 1) //there are aliases
						{
							result += $" {Utilities.Transform(Resources.help_aliases)}";

							foreach(string s in c.Aliases())
							{
								if(s.Equals(c.CommandTrigger()))
									continue;

								result += $"{s}, ";
							}

							Utilities.SendMessage(user, $"  {result.Substring(0, result.LastIndexOf(','))})");
							break;
						}

						Utilities.SendMessage(user, $"  {result}");
					}
				}
				else
					Utilities.SendMessage(user, $"  {Resources.help_none}");

				Utilities.SendMessage(user, Resources.help_privateCommands);

				if(m.PrivateCmds.Count > 0)
				{
					foreach(BasePrivateCommand c in m.PrivateCmds)
					{
						Utilities.SendMessage(user, $"  {c.Syntax()}");
					}
				}
				else
					Utilities.SendMessage(user, $"  {Resources.help_none}");
			
				Utilities.SendMessage(user, Resources.help_usageHeader);

				foreach(string s in m.Usage())
				{
					Utilities.SendMessage(user, $"  {s}");
				}
				
				Utilities.SendMessage(user, Resources.help_notesHeader);
				Utilities.SendMessage(user, $"  {m.Notes()}");
			}
			else
				Utilities.SendMessage(user, $"  {Resources.help_noPermission}");
		}
	}
}
