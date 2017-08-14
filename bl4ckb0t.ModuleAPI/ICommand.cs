using ChatSharp;

namespace bl4ckb0t.ModuleAPI
{
	/// <summary>
	/// Models a command that is sent via IRC. Use this if you want to handle commands from private messages
	/// </summary>
	/// <typeparam name="E">The type of event a command got sent with (e.g. ChatSharp.Events.PrivateMessageEventArgs)</typeparam>
	public interface ICommand
	{
		/// <summary>
		/// What happens when the command gets executed
		/// </summary>
		/// <param name="e">The class that holds information about the command sender, the source, the message, etc.</param>
		/// <param name="cmdName">The command itself</param>
		/// <param name="args">The arguments of the command</param>
		/// <returns>true if the command has been correctly used, false if the help menu should be shown</returns>
		bool Exe(PrivateMessage e, string cmdName, string[] args);

		/// <summary>
		/// The syntax of the command as in how it has to be used
		/// </summary>
		/// <returns>The syntax</returns>
		string Syntax();

		/// <summary>
		/// The trigger of the command (e.g. ping would be the trigger, while pong would only be an alias)
		/// </summary>
		/// <returns>The trigger without the command prefix</returns>
		string CommandTrigger();
	}
}
