using System;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System.Text;
using NSoup.Nodes;
using System.Text.RegularExpressions;

namespace bl4ckb0t.Modules.YouTubeInfo
{
	public class YouTubeInfo : BaseModule, ILinkAction
	{
		public YouTubeInfo(string _name) : base(_name) { }

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
			string yt = "";
			string title = Resources.noValue;
			string duration = Resources.noValue;
			string views = Resources.noValue;
			string likes = Resources.noValue;
			string dislikes = Resources.noValue;
			string date = Resources.noValue;
			string uploader = Resources.noValue;

			if(link.Contains("watch?v="))
			{
				try
				{
					yt = $"http://www.youtube.com/watch?v={Utilities.Split(link, "v=")[1].Substring(0, 11)}/";
				}
				catch(ArgumentOutOfRangeException)
				{
					goto noID;
				}
			}
			else if(link.Contains("youtu.be/"))
				yt = $"http://www.youtube.com/watch?v={Utilities.Split(link, "youtu.be/")[1]}/";
			else
			goto noID;

			//check the length of the link (it's fixed because a video id is always a fixed length) and if it's larger, concatenate it onto the correct length
			if(yt.Length > 42)
				yt = new StringBuilder().Append(yt).Remove(42, yt.Length - 42).ToString();
			else if(yt.Length < 42)
				goto noID;

			Document doc = WebWrapper.NewNSoup(yt);

			title = doc.Select("#eow-title")[0].Text();

			foreach(Element el in doc.GetElementsByTag("script"))
			{
				foreach(DataNode n in el.DataNodes)
				{
					if(n.GetWholeData().Contains("length_seconds"))
					{
						foreach(string s in n.GetWholeData().Split(','))
						{
							if(s.StartsWith("\"length_seconds\""))
							{
								int seconds = int.Parse(s.Split(':')[1].Replace("\"", "").Replace("}", ""));
								int minutes = 0;
								int hours = 0;

								while(seconds >= 60)
								{
									seconds -= 60;
									minutes++;
								}

								while(minutes >= 60)
								{
									minutes -= 60;
									hours++;
								}

								duration = $"{(hours < 10 ? $"0{hours}" : $"{hours}")}:{(minutes < 10 ? $"0{minutes}" : $"{minutes}")}:{(seconds < 10 ? $"0{seconds}" : $"{seconds}")}";
								goto finishLoop;
							}
						}
					}
				}
			}

			finishLoop: { }

			try
			{
				views = Regex.Replace(doc.Select(".watch-view-count")[0].Text(), "[^0-9+.]" ,"");
			}
			catch(IndexOutOfRangeException)
			{
				views = Resources.ratingDisabled;
			}

			try
			{
				likes = doc.Select(".like-button-renderer-like-button-unclicked")[0].Text();
				dislikes = doc.Select(".like-button-renderer-dislike-button-unclicked")[0].Text();

				if(likes.Equals(""))
					likes = Resources.ratingDisabled;

				if(dislikes.Equals(""))
					dislikes = Resources.ratingDisabled;
			}
			catch(IndexOutOfRangeException)
			{
				likes = Resources.ratingDisabled;
				dislikes = Resources.ratingDisabled;
			}

			date = Regex.Replace(doc.Select(".watch-time-text")[0].Text(), "^[^0-9]*", "");
			uploader = doc.Select(".yt-user-info > .g-hovercard")[0].Text();
			Utilities.SendStarMsg(channel,
				$"{Colors.WithBackground(Colors.WHITE, Colors.BLACK)}You{Colors.WithBackground(Colors.RED, Colors.WHITE)}Tube",
				Utilities.Transform(Resources.title, title),
				Utilities.Transform(Resources.duration, duration),
				Utilities.Transform(Resources.views, views),
				Utilities.Transform(Resources.likes, likes),
				Utilities.Transform(Resources.dislikes, dislikes),
				Utilities.Transform(Resources.uploader, uploader),
				Utilities.Transform(Resources.date, date));
			return;

			noID: {
				Utilities.SendMessage(channel, Resources.noId);
			}
		}

		public bool IsValid(string link)
		{
			return link.Contains("www.youtube.com/watch") || link.Contains("youtu.be/");
		}

		public int Priority()
		{
			return 60;
		}
	}
}
