using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace bl4ckb0t.Modules.Remind
{
	public class Remind : BaseModule
	{
		public static readonly string path = new NoPrefixUri(Path.Combine(Utilities.DataPath(), "reminders")).LocalPath;
		private Dictionary<string, string> buffer = new Dictionary<string, string>();

		public Remind(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			RegisterChannelCommand(new Command());

			if(!Directory.Exists(path))
				Directory.CreateDirectory(new NoPrefixUri(path).LocalPath);

			foreach(string f in Directory.EnumerateFiles(path))
			{
				List<string> contents = Utilities.ReadLines(new StreamReader(f), true);
				string user = contents[1].Split(':')[1];
				string remindOf = contents[2].Split(':')[1];
				string channel = contents[3].Split(':')[1];
				long time = long.Parse(contents[0].Split(':')[1]) - Utilities.CurrentTimeMillis();
				Reminder r;

				if(time <= 0)
				{
					buffer[channel] = Utilities.Transform(Resources.reminderOver, user, remindOf, Utilities.UnixToFormat(time, Resources.timeFormat));
					File.Delete(f);
					goto cont;
				}

				r = new Reminder(Utilities.ToTimeSpan(Utilities.UnixToFormat(Utilities.CurrentTimeMillis() - long.Parse(contents[0].Split(':')[1]), "{0}d{1}h{2}m{3}s")), user, remindOf, channel);

				if(r.Id != int.Parse(Utilities.Split(f.Substring(f.LastIndexOf(@"\") + 1), ".txt")[0]))
				{
					buffer[channel] = Utilities.Transform(Resources.newId, user, remindOf, r.Id);
					File.Delete(f);
				}

				cont: { }
			}
		}

		public override void OnJoined()
		{
			foreach(string s in buffer.Keys)
			{
				Utilities.SendMessage(s, buffer[s]);
			}

			buffer.Clear();
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3 };
		}
	}
}
