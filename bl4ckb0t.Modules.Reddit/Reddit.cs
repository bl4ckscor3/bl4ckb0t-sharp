using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using NSoup.Nodes;
using NSoup.Select;
using System;

namespace bl4ckb0t.Modules.Reddit
{
	public class Reddit : BaseModule, ILinkAction
	{
		public Reddit(string _name) : base(_name) { }

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
            if(link.Contains("/user/") || link.Contains("/u/"))
                User(channel, link.Replace("/u/", "/user/"));
            else
            {
                string noPrefixLink = link.Replace("http://", "").Replace("https://", "").Replace("www.", "");

                if(!noPrefixLink.StartsWith("reddit.com")) //the link is of the format subredditnamehere.reddit.com
                {
                    link = $"https://www.reddit.com/r/{Utilities.Split(noPrefixLink, ".reddit")[0]}";

                    if(Utilities.Split(noPrefixLink, ".com").Length >1)
                        link += Utilities.Split(noPrefixLink, ".com")[1];
                }

                if(link.Contains("/r/"))
                {
                    if(link.Contains("/comments/"))
                    {
						if(link.EndsWith("/"))
							link = link.Substring(0, link.LastIndexOf('/'));

						Document doc = WebWrapper.NewNSoup(link);
						string title = doc.Select("a.title")[0].Text().Replace(" ", "_").ToLower();
						string[] split = link.Split('/');
						string linkTitle = split[split.Length - 1];

						if(title.StartsWith(linkTitle))
							Post(channel, link, doc);
						else
							Comment(channel, link, doc);
					}
                    else
                        Subreddit(channel, link);
                }
            }
        }

        /// <summary>
        /// Shows information about the reddit user
        /// </summary>
        /// <param name="channel">The channel to send the information in</param>
        /// <param name="link">The link to the user's profile</param>
        private void User(string channel, string link)
        {
            Document doc = WebWrapper.NewNSoup(link);

            Utilities.SendStarMsg(channel,
                $"/u/{link.Split('/')[4]}",
                Utilities.Transform(Resources.postKarma, doc.Select(".karma")[0].Text()),
				Utilities.Transform(Resources.commentKarma, doc.Select(".comment-karma").Text),
				Utilities.Transform(Resources.dateCreated, doc.Select(".age > time").Attr("title")));
		}

		/// <summary>
		/// Shows information about the subreddit
		/// </summary>
		/// <param name="channel">The channel to send the information in</param>
		/// <param name="link">The link to the subreddit</param>
		private void Subreddit(string channel, string link)
		{
			Document doc = WebWrapper.NewNSoup(link);

			Utilities.SendStarMsg(channel,
				$"/r/{doc.Select(".redditname > a").Text.Split(' ')[0]}",
				Utilities.Transform(Resources.subscribers, doc.Select(".subscribers > .number").Text),
				Utilities.Transform(Resources.viewers, doc.Select(".users-online > .number").Text));
		}

		/// <summary>
		/// Shows information about a selfpost or linked post
		/// </summary>
		/// <param name="channel">The channel to send the information in</param>
		/// <param name="link">The link to the comment</param>
		/// <param name="doc">The NSoup Document with the link loaded</param>
		private void Post(string channel, string link, Document doc)
		{
			string gilded = doc.Select(".gilded-icon").Attr("data-count");
			string votes = doc.Select("div.score > span.number").Text;
			string percentage = Utilities.Split(doc.Select(".score")[0].Text(), "(")[1].Split('%')[0] + "%";
			string title = doc.Select("a.title")[0].Text();
			string type = doc.Select(".domain")[0].Text().Contains("self.") ? "(Selfpost)" : doc.Select(".domain")[0].Text();
			Elements allComments = doc.Select($".comments");
			string comments = allComments[allComments.Count - 1].Text();
			string author = doc.Select("p.tagline > a.author")[0].Text();
			string time = doc.Select("p.tagline > time")[0].Text();

			if (gilded.Equals(""))
				gilded = "0";

			Utilities.SendMessage(channel, $"{Colors.BROWN}({gilded}) {Colors.GREEN}[{votes} {percentage}] {Colors.NORMAL}{title} {Colors.ITALICS}{Colors.LIGHT_GRAY}{type}{Colors.NORMAL} - {comments} - {Colors.MAGENTA}/u/{author} posted {time}");
		}

		/// <summary>
		/// Shows information about a comment
		/// </summary>
		/// <param name="channel">The channel to send the information in</param>
		/// <param name="link">The link to the comment</param>
		/// <param name="doc">The NSoup Document with the link loaded</param>
		private void Comment(string channel, string link, Document doc)
		{
			Element comment = doc.Select($"#thing_t1_{link.Substring(link.LastIndexOf('/') + 1)} > div.entry > p.tagline")[0];
			string gilded;
			string author = comment.Select("a.author").Text;
			string votes = comment.Select("span.unvoted").Text;
			string time = comment.Select("time").Text;

			try
			{
				gilded = doc.Select(".gilded-icon")[1].Attr("data-count"); //if the author has a gild
			}
			catch(ArgumentOutOfRangeException)
			{
				gilded = doc.Select(".gilded-icon").Attr("data-count"); //if the op has no gild
			}

			if(gilded.Equals(""))
				gilded = "0";

			Utilities.SendMessage(channel, $"{Colors.BROWN}({gilded}) {Colors.GREEN}[{votes}] {Colors.MAGENTA}/u/{author} posted {time}");
		}

		public bool IsValid(string link)
        {
            return link.Contains("reddit.com");
        }

        public int Priority()
        {
            return 40;
        }
	}
}
