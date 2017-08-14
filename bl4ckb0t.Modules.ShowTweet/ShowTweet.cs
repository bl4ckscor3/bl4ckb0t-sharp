using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using NSoup.Nodes;
using NSoup;
using NSoup.Select;
using System;

namespace bl4ckb0t.Modules.ShowTweet
{
	public class ShowTweet : BaseModule, ILinkAction
	{
		public ShowTweet(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			LinkManager.RegisterLinkAction(this);
		}

		public override void OnDisable(Bot client)
		{
			LinkManager.RemoveLinkAction(this);
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}

		public void Handle(string channel, string user, string link)
		{
			Show(channel, user, link, 0);
		}

		/// <summary>
		/// Shows a Tweet
		/// </summary>
		/// <param name="channel">The channel to show the Tweet in</param>
		/// <param name="user">The user who sent the link</param>
		/// <param name="link">The link to the Tweet</param>
		/// <param name="depth">Recursive depth</param>
		private void Show(string channel, string user, string link, int depth)
		{
			string name = "";
			string account = "";
			string tweet = "";
			bool verified = false;

			if(link.Split('/').Length < 6) //it's not a tweet, tweets have at least 6 sections
				return;

			Document doc = null;

			try
			{
				doc = WebWrapper.NewNSoup(link);
			}
			catch(HttpStatusException ex)
			{
				if(ex.StatusCode == 404)
					Utilities.SendMessage(channel, Resources.notFound);
				else
				{
					Logger.StackTrace(ex);
					Logger.StackTrace(ex);
					Utilities.SendMessage(channel, Resources.error);
				}

				return;
			}

			try
			{
				name = doc.Select(".permalink-header > .account-group > .FullNameGroup > .fullname")[0].OwnText();

				if(doc.Select(".permalink-header > .account-group > .FullNameGroup > .UserBadges > .Icon--verified").Count != 0)
					verified = true;
			}
			catch(ArgumentOutOfRangeException ex)
			{
				if(doc.Select("div.ProtectedTimeline").Count != 0)
					Utilities.SendMessage(channel, Resources.protectedTweets);
				else
				{
					Logger.StackTrace(ex);
					Utilities.SendMessage(channel, Resources.error);
				}

				return;
			}

			Elements replyingTo = doc.Select(".permalink-tweet > .ReplyingToContextBelowAuthor");

			account = doc.Select(".permalink-header > .account-group > .username")[0].Text();
			tweet = doc.Select(".TweetTextSize--jumbo")[0].Text().Replace("https://twitter.com", " https://twitter.com").Replace("pic.twitter", " pic.twitter").Replace(" ", "").Trim();

			//adding the people this tweet got sent in reply to, if any
			if(replyingTo.Count != 0)
			{
				foreach(Element el in replyingTo.Select(".js-user-profile-link"))
				{
					tweet = $"{el.Text()} {tweet}";
				}
			}

			//removing the horizontal ellipsis () because somehow it doesn't work inline -.-
			if(tweet.IndexOf('\u2026') > -1)
				tweet = tweet.Replace("\u2026", "");

			string msg = $"{Colors.BOLD}{name} ({account}) {(verified ? "\u2713 " : "")}- {Colors.NORMAL}{tweet}";

			//adding nested tweet prefixes
			for(int i = depth; i > 0; i--)
			{
				if(i == depth)
					msg = $"> {msg}";
				else
					msg = $" {msg}";
			}

			Elements vote = doc.Select(".card2");
			bool hasVote = false;

			foreach(Element el in vote.ToArray())
			{
				if(el.HasAttr("data-card2-name") && Utilities.Matches(el.Attr("data-card2-name"), "poll[0-9]+choice_text_only"))
					hasVote = true;
			}

			Utilities.SendMessage(channel, msg + (hasVote ? $"{Colors.ITALICS} ({Resources.vote})" : ""));

			string[] split = tweet.Split(' ');

			try
			{
				for(int i = 0; i < split.Length; i++)
				{
					if(split[i].Contains("twitter.com"))
						Show(channel, user, split[i], depth + 1);
				}
			}
			catch(IndexOutOfRangeException) { } //happens when the Tweet doesn't actually have any quoted Tweet in it

			string links = "";

			//extracting all links from the tweet
			foreach(Element el in doc.Select(".TweetTextSize--jumbo")[0].Select(".twitter-timeline-link"))
			{
				if(!el.Attr("data-expanded-url").Contains("twitter.com")) //ignore nested tweets (and twitter links in general but who tf cares)
					links += $"{el.Attr("data-expanded-url")} ";
			}

			LinkManager.HandleLink(links.Trim(), channel, user);
		}

		public bool IsValid(string link)
		{
			return link.Contains("twitter.com");
		}

		public int Priority()
		{
			return 100;
		}
	}
}
