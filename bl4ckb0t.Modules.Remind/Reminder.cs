using bl4ckb0t.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace bl4ckb0t.Modules.Remind
{
	public class Reminder
	{
		public static List<Reminder> reminders = new List<Reminder>();
		private static int lastId = 0;
		private int id;
		public int Id { get { return id; } }
		private TimeSpan timeUntil;
		public TimeSpan TimeUntil { get { return timeUntil; } }
		private string user;
		public string User { get { return user; } }
		private string remindOf;
		public string RemindOf { get { return remindOf; } }
		private string channel;
		public string Channel { get { return channel; } }
		private Timer timer;
		public Timer Timer { get { return timer; } }
		private string path;

		public Reminder(TimeSpan _timeUntil, string _user, string _remindOf, string _channel)
		{
			timer = new Timer(SendReminder, null, (long)_timeUntil.TotalMilliseconds, Timeout.Infinite);
			id = ++lastId;
			timeUntil = _timeUntil;
			user = _user;
			remindOf = _remindOf;
			channel = _channel;
			path = new NoPrefixUri(Path.Combine(Remind.path, $"{id}.txt")).LocalPath;
			Write();
			reminders.Add(this);
		}

		/// <summary>
		/// Reminds a user of something once the timer ran out
		/// </summary>
		/// <param name="t">The Task</param>
		private void SendReminder(object state)
		{
			Stop();
			Utilities.SendMessage(channel, Resources.reminder, user, remindOf);
		}

		/// <summary>
		/// Writes this reminder to a file
		/// </summary>
		private void Write()
		{
			if(!File.Exists(path))
			{
				File.Create(path).Close();
				Utilities.WriteLines(new StreamWriter(path), new List<string>() {
					$"due:{Utilities.CurrentTimeMillis() + timeUntil.TotalMilliseconds}",
					$"user:{user}",
					$"remindOf:{remindOf}",
					$"channel:{channel}" }, true);
			}
		}

		/// <summary>
		/// Gets the Reminder with the given ID
		/// </summary>
		/// <param name="id">The ID of the Reminder to get</param>
		/// <returns>The Reminder, null if none has been found</returns>
		public static Reminder Get(int id)
		{
			foreach(Reminder r in reminders)
			{
				if(r.Id == id)
					return r;
			}

			return null;
		}

		/// <summary>
		/// Stops this reminder
		/// </summary>
		public void Stop()
		{
			timer.Dispose();
			reminders.Remove(this);
			File.Delete(path);
		}
	}
}
