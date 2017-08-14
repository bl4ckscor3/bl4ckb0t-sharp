using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace bl4ckb0t.Modules.GitHubInfo
{
	public class Issue : ILinkAction
	{
		public void Handle(string channel, string user, string link)
		{
			string name = $"{link.Split('/')[3]}/{link.Split('/')[4]}";
			string number = Utilities.Split(link, link.Contains("issues") ? "issues/" : "pull/")[1].Replace("/", "");
			WebClient client = WebWrapper.NewClient();
			JObject o;
			JArray json;

			if(number.Contains("#"))
				number = number.Split('#')[0];

			o = JObject.Parse(client.DownloadString($"https://api.github.com/repos/{name}/issues/{number}"));
			json = o["array"].Value<JArray>();
			Utilities.SendStarMsg(channel,
				Utilities.Transform(Resources.issue_header, name, (json.Contains("pull_request") ? Resources.issue_pullRequest : Resources.issue_issue), number),
				Utilities.Transform(Resources.issue_title, json["title"].Value<string>()),
				Utilities.Transform(Resources.issue_createdBy, json["user"]["login"].Value<string>()),
				Utilities.Transform(Resources.issue_state, json["state"].Value<string>()),
				Utilities.Transform(Resources.issue_comments, json["comments"].Value<string>()));
			client.Dispose();
		}

		public bool IsValid(string link)
		{
			return link.Contains("github.com") && (link.Contains("issues") || link.Contains("pull"));
		}

		public int Priority()
		{
			return 80;
		}
	}
}
