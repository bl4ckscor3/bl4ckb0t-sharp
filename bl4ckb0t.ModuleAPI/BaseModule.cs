using bl4ckb0t.BotInformation;
using bl4ckb0t.Util;
using System.Collections.Generic;

namespace bl4ckb0t.ModuleAPI
{
	public abstract class BaseModule
	{
		/// <summary>
		/// Whether this module is a private module (shipped with the bot) or not
		/// </summary>
		private bool privateModule;
		public bool PrivateModule { get { return privateModule; } }
		/// <summary>
		/// All channel commands registered to this module
		/// </summary>
		private List<BaseChannelCommand> channelCmds;
		public List<BaseChannelCommand> ChannelCmds { get { return channelCmds; } }
		/// <summary>
		/// All private message commands registered to this module
		/// </summary>
		private List<BasePrivateCommand> privateCmds;
		public List<BasePrivateCommand> PrivateCmds { get { return privateCmds; } }
		/// <summary>
		/// The name of the module
		/// </summary>
		private string name;
		public string Name { get { return name; } }

		/// <summary>
		/// Sets up this module
		/// </summary>
		/// <param name="_name">The name of this module</param>
		public BaseModule(string _name)
		{
			privateModule = ModuleHandler.loadingPrivateModules;
			channelCmds = new List<BaseChannelCommand>();
			privateCmds = new List<BasePrivateCommand>();
			name = _name;
		}

		/// <summary>
		/// Gets called when the module gets enabled. Should be used to add any custom listeners or commands. Note that you need to remove any custom listeners in OnDisable()
		/// </summary>
		/// <param name="client">The client that the module can use to add custom listeners</param>
		public abstract void OnEnable(Bot client);

		/// <summary>
		/// Gets called when the module gets disabled. Should be used to remove any Listeners
		/// <param name="client">The client that the module needs to use to remove listeners and clean up before shutting down. This is the same one as is passed in onEnable()</param>
		/// </summary>
		public virtual void OnDisable(Bot client) { }

		/// <summary>
		/// Gets called when all modules have been loaded
		/// </summary>
		public virtual void OnFinish() { }

		/// <summary>
		/// Gets called when the bot has joined all channels once connected
		/// </summary>
		public virtual void OnJoined() { }

		/// <summary>
		/// Gets called when the -update command is executed
		/// </summary>
		public virtual void OnUpdate() { }

		/// <summary>
		/// Explanation of the module, gets shown in the help menu
		/// You don't need to call Utilities.Transform() on each element, this is done automatically in the help menu routine
		/// </summary>
		/// <returns>A string array of lines to display in the help menu</returns>
		public abstract string[] Usage();

		/// <summary>
		/// Anything special the user needs to know about the module, gets shown in the help menu
		/// You don't need to call Utilities.Transform() on the return value, this is done automatically in the help menu routine
		/// </summary>
		/// <returns>A string that gets displayed in the help menu</returns>
		public virtual string Notes()
		{
			return Resources.defaultNotes;
		}

		/// <summary>
		/// Defines which permission level is needed to use this module
		/// </summary>
		/// <returns>You should return either 1, 2 or 3, 1 being the lowest and 3 the highest</returns>
		public virtual int RequiredPermissionLevel()
		{
			return 1;
		}

		/// <summary>
		/// Checks wether a user has permission to execute this command
		/// </summary>
		/// <param name="user">The name of the user to check</param>
		/// <returns>true if they have permission, false otherwise</returns>
		public bool HasPermission(string user)
		{
			return Lists.GetPermissionLevel(user) >= RequiredPermissionLevel();
		}

		/// <summary>
		/// Registers a channel command to this module
		/// </summary>
		/// <param name="c">The command to register</param>
		public void RegisterChannelCommand(BaseChannelCommand c)
		{
			if(!ChannelCmds.Contains(c))
				ChannelCmds.Add(c);
		}
		
		/// <summary>
		/// Registers a private command to this module
		/// </summary>
		/// <param name="c">The command to register</param>
		public void RegisterPrivateCommand(BasePrivateCommand c)
		{
			if(!PrivateCmds.Contains(c))
				PrivateCmds.Add(c);
		}
	}
}
