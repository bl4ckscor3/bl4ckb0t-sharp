using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;

namespace bl4ckb0t.Modules.Action
{
	public class Action : BaseModule
	{
		public Action(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterPrivateCommand(new ActionCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}
	}

	public class ActionCommand : BasePrivateCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			Utilities.Client().Action(args[0], Utilities.ConcatAt(args, 1));
			return true;
		}

		public override string CommandTrigger()
		{
			return "*";
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
