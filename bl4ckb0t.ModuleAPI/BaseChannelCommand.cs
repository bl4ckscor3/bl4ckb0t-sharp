using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.ModuleAPI
{
	public abstract class BaseChannelCommand : ICommand
	{
		public abstract bool Exe(PrivateMessage e, string cmdName, string[] args);

		public abstract string Syntax();

		public string CommandTrigger()
		{
			return Aliases()[0];
		}

		/// <summary>
		/// Additional triggers that can be used to execute the command (e.g. pong would be such a trigger, while ping would not as it is the main trigger)
		/// In this case, the first trigger specified will be used as the main trigger
		/// </summary>
		/// <returns>All aliases</returns>
		public abstract string[] Aliases();

		/// <summary>
		/// Checks if a string is a valid alias for this command
		/// </summary>
		/// <param name="alias">The alias to check WITHOUT the command prefix</param>
		/// <returns>true if the string can be used to trigger the command, false otherwise</returns>
		public bool IsValidAlias(string alias)
		{
			foreach(string a in Aliases())
			{
				if(Utilities.EqualsIgnoreCase(a, alias))
					return true;
			}

			return false;
		}
	}
}
