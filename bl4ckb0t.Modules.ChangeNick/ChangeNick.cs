using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp.Events;

namespace bl4ckb0t.Modules.ChangeNick
{
	/// <summary>
	/// I personally hate this module
	/// </summary>
	public class ChangeNick : BaseModule
	{
		public static string lastChannel = ""; //the last channel used to execute this command

		public ChangeNick(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());
			client.NickInUse += HandleInUse;
			client.NickChanged += HandleChanged;
		}

		/// <summary>
		/// Handles a nick already being in use
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		private void HandleInUse(object c, ErronousNickEventArgs e)
		{
			e.DoNotHandle = true;
			Utilities.SendMessage(lastChannel, Resources.inUse);
		}

		private void HandleChanged(object c, NickChangedEventArgs e)
		{
			if(e.OldNick.Equals(Utilities.Client().User.Nick))
				Utilities.Client().User.Nick = e.NewNick;
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2 };
		}

		public override int RequiredPermissionLevel()
		{
			return 3;
		}
	}
}
