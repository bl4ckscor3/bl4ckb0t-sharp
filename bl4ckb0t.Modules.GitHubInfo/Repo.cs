using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using Newtonsoft.Json.Linq;
using System.Net;

namespace bl4ckb0t.Modules.GitHubInfo
{
	public class Repo : ILinkAction
	{
		public static int count = 0;

		public void Handle(string channel, string user, string link)
		{
			string name = $"{link.Split('/')[3]}/{link.Split('/')[4]}";
			WebClient client = WebWrapper.NewClient();
			JObject jObject = new JObject();
			jObject = JObject.Parse(client.DownloadString($"https://api.github.com/repos/{name}"));
			JArray json = jObject["array"].Value<JArray>();
			string[] split;
			string latestPush = "";
			string language = json["language"].Value<string>();

			if(language.Equals("null") || language.Equals(""))
				language = Resources.none;

			if(link.Split('/').Length > 6 && link.Split('/')[5].Equals("tree"))
			{
				name += "/branches/" + link.Split('/')[6];
				jObject = JObject.Parse(client.DownloadString($"https://api.github.com/repos/{name}"));
				split = jObject["array"]["commit"]["commit"]["commiter"]["date"].Value<string>().Split(':');
				latestPush = $"{split[1].Replace("\"", "").Replace("T", " ")}:{split[2]}:{split[3].Replace("Z", "").Replace("\"", "").Replace("}", "")} GMT";
			}
			else
			{
				split = json["pushed_at"].Value<string>().Split(':');
				latestPush = split[1].Replace("\"", "").Replace("T", " ") + ":" + split[2] + ":" + split[3].Replace("Z", "").Replace("\"", "") + " GMT";
			}

			Utilities.SendStarMsg(channel,
			Utilities.Transform(Resources.repo_header, name, json["description"].Value<string>()),
			Utilities.Transform(Resources.repo_mainLanguage, language),
			Utilities.Transform(Resources.repo_latestPush, latestPush),
			Utilities.Transform(Resources.repo_watching, json["watchers_count"].Value<string>()),
			Utilities.Transform(Resources.repo_stargazers, json["stargazers_count"].Value<string>()),
			Utilities.Transform(Resources.repo_forks, json["forks_count"].Value<string>()),
			Utilities.Transform(Resources.repo_issues, json["open_issues_count"].Value<string>()));
			client.Dispose();
		}

		public bool IsValid(string link)
		{
			return link.Contains("github.com");
		}

		public int Priority()
		{
			return 70;
		}
	}
}
