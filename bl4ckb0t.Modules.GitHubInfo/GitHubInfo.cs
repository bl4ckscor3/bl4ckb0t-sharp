using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;

namespace bl4ckb0t.Modules.GitHubInfo
{
	public class GitHubInfo : BaseModule
	{
		private ILinkAction commit;
		private ILinkAction issue;
		private ILinkAction repo;

		public GitHubInfo(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			LinkManager.RegisterLinkAction(commit = commit ?? new Commit()); //only set this if commit is null
			LinkManager.RegisterLinkAction(issue = issue ?? new Issue());
			LinkManager.RegisterLinkAction(repo = repo ?? new Repo());
		}

		public override void OnDisable(Bot client)
		{
			LinkManager.RemoveLinkAction(commit);
			LinkManager.RemoveLinkAction(issue);
			LinkManager.RemoveLinkAction(repo);
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}
	}
}
