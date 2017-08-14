using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace bl4ckb0t.Modules.LinkTitle
{
	public class LinkTitle : BaseModule, ILinkAction
	{
		private List<string> blacklistedWebsites = new List<string>();

		public LinkTitle(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			FetchBlacklistedWebsites();
			LinkManager.RegisterLinkAction(this);
		}

		public override void OnDisable(Bot client)
		{
			LinkManager.RemoveLinkAction(this);
		}

		public override void OnUpdate()
		{
			blacklistedWebsites.Clear();
			FetchBlacklistedWebsites();
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}

		public void Handle(string channel, string user, string link)
		{
			string title = WebWrapper.NewNSoup(link).Title;

			link = link.Substring(link.IndexOf("//") + 2);

			if(link.Length > 21)
			{
				link = link.Substring(0, 21);
				link += "...";
			}

			if(title.Equals(null) || title.Equals("") || title.Equals("null"))
				Utilities.SendMessage(channel, Resources.notFound, link);
			else
				Utilities.SendMessage(channel, Resources.reply, link, title);
		}

		public bool IsValid(string link)
		{
			foreach(string s in blacklistedWebsites)
			{
				if(link.Contains(s))
					return false;
			}

			return true;
		}

		public int Priority()
		{
			return 0;
		}

		/// <summary>
		/// Populates the blacklistedWebsites list
		/// </summary>
		private void FetchBlacklistedWebsites()
		{
			WebClient webClient = WebWrapper.NewClient();
			var f = new StreamReader(webClient.OpenRead("https://akino.canopus.uberspace.de/bl4ckb0t/files/blacklistedWebsites.txt"));
			string line = "";

			while((line = f.ReadLine()) != null)
			{
				blacklistedWebsites.Add(line);
			}

			webClient.Dispose();
			f.Close();
		}
	}
}
