using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace bl4ckb0t.Modules.Remind
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length == 0)
				return false;

			string channel = e.Source;
			string user = e.User.Nick;
			int number;

			if(args.Length >= 2)
			{
				if(args.Length == 2 && Utilities.EqualsIgnoreCase(args[0], "stop") && int.TryParse(args[1], out number))
				{
					Reminder r = Reminder.Get(number);

					if(r == null)
					{
						Utilities.SendMessage(channel, Resources.notExisting);
						return true;
					}
					else if(!r.User.Equals(user))
					{
						Utilities.SendMessage(channel, Resources.noPermission);
						return true;
					}

					r.Stop();
					Utilities.SendMessage(channel, Resources.stopped, number);
				}
				else
				{
					TimeSpan span = Utilities.ToTimeSpan(args[0]);
					Reminder r;

					if(span.TotalMilliseconds == 0)
						return false;

					r = new Reminder(span, user, Utilities.ConcatAt(args, 1), channel);
					Utilities.SendMessage(channel, Resources.success, r.Id);
				}
			}
			else if(args.Length == 1)
			{
				if(int.TryParse(args[0], out number))
				{
					Reminder r = Reminder.Get(number);

					if(r == null)
					{
						Utilities.SendMessage(channel, Resources.notExisting);
						return true;
					}
					else if(!r.User.Equals(user))
					{
						Utilities.SendMessage(channel, Resources.noPermission);
						return true;
					}

					Utilities.SendMessage(channel, Resources.info, r.RemindOf, Utilities.UnixToFormat((long)r.TimeUntil.TotalMilliseconds, Resources.timeFormat));
				}
				else if(Utilities.EqualsIgnoreCase(args[0], "list"))
				{
					int count = 0;
					string ids = "";

					foreach(string f in Directory.EnumerateFiles(Remind.path))
					{
						List<string> contents = Utilities.ReadLines(new StreamReader(f), true);

						if(Utilities.EqualsIgnoreCase(contents[1].Split(':')[1], user))
						{
							count++;
							ids += $"{int.Parse(Utilities.Split(f.Substring(f.LastIndexOf(@"\") + 1), ".txt")[0])}, ";
						}
					}

					if(count == 0)
						Utilities.SendMessage(channel, Resources.noneActive);
					else
						Utilities.SendMessage(channel, Resources.list, count, Utilities.Substring(ids, 0, ids.LastIndexOf(", ")));
				}
				else
					return false;
			}
			else
				return false;
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "remind" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
