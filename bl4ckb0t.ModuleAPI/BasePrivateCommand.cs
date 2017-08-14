using ChatSharp;

namespace bl4ckb0t.ModuleAPI
{
	public abstract class BasePrivateCommand : ICommand
	{
		public abstract bool Exe(PrivateMessage msg, string cmdName, string[] args);
		public abstract string CommandTrigger();
		public abstract string Syntax();
	}
}
