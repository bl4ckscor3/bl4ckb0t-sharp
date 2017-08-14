using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using Newtonsoft.Json.Linq;
using System.Net;

namespace bl4ckb0t.Modules.GitHubInfo
{
	public class Commit : ILinkAction
	{
		public void Handle(string channel, string user, string link)
		{
			string title = WebWrapper.NewNSoup(link).Title;
			string sha = title.Substring(title.LastIndexOf('@') + 1).Split(' ')[0];
			string name = Utilities.Substring(title, 0, title.LastIndexOf('@')).Split(' ')[Utilities.Substring(title, 0, title.LastIndexOf('@')).Split(' ').Length -1];
			WebClient client = WebWrapper.NewClient();
			JObject o = JObject.Parse(client.DownloadString($"https://api.github.com/repos/{name}/commits/{sha}"));
			JArray json = o["array"].Value<JArray>();

			Utilities.SendStarMsg(channel,
				Utilities.Transform(Resources.commit_changedFiles, json["files"].Value<JArray>().Count),
				Utilities.Transform(Resources.commit_additions, json["stats"]["additions"].Value<int>()),
				Utilities.Transform(Resources.commit_deletions, json["stats"]["deletions"].Value<int>()));
			client.Dispose();
		}

		public bool IsValid(string link)
		{
			return link.Contains("git.io") || (link.Contains("github.com") && link.Contains("commit"));
		}

		public int Priority()
		{
			return 90;
		}
	}
}
