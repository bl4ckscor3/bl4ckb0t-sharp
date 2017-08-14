using System;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System.Collections.Generic;
using System.IO;

namespace bl4ckb0t.Modules.CookieRewards
{
	public class CookieCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string nick = e.User.Nick;
			string channel = e.Source;

			if(args.Length == 0 || args.Length > 2)
				return false;

			if(Utilities.EqualsIgnoreCase(nick, args[0]))
			{
				Utilities.SendMessage(channel, Resources.givingSelf);
				return true;
			}

			List<string> contents = Utilities.ReadLines(new StreamReader(CookieRewards.path), true);
			bool senderModified = false;
			bool receiverModified = false;

			for(int i = 0; i < contents.Count; i++)
			{
				try
				{
					string name = contents[i].Split('#')[0];
					int amount = int.Parse(contents[i].Split('#')[1]);
					int given = int.Parse(contents[i].Split('#')[2]);
					int received = int.Parse(contents[i].Split('#')[3]);

					if(Utilities.EqualsIgnoreCase(name, nick))
					{
						if(args.Length == 2)
						{
							int value = int.Parse(args[1]);

							amount -= value;
							given += value;
						}
						else
						{
							amount--;
							given++;
						}

						if(amount < 0)
						{
							Utilities.SendMessage(channel, Resources.notEnough);
							return true;
						}

						contents.Remove(contents[i]);
						contents.Insert(i, $"{name}#{amount}#{given}#{received}");
						senderModified = true;
					}
					else if(Utilities.EqualsIgnoreCase(name, args[0]))
					{
						if(args.Length == 2)
						{
							int value = int.Parse(args[1]);

							amount += value;
							received += value;
						}
						else
						{
							amount++;
							received++;
						}

						contents.Remove(contents[i]);
						contents.Insert(i, $"{name}#{amount}#{given}#{received}");
						receiverModified = true;
					}
				}
				catch(Exception)
				{
					return false;
				}
			}

			//these two cases only happen when the respective user isn't in the database yes
			if(!senderModified)
			{
				try
				{
					int amount = 20;
					int given = 0;

					if(args.Length == 2)
					{
						int value = int.Parse(args[1]);

						amount -= value;
						given += value;
					}
					else
					{
						amount--;
						given++;
					}

					if(amount < 0)
					{
						Utilities.SendMessage(channel, Resources.notEnough);
						return true;
					}

					contents.Add($"{nick}#{amount}#{given}#0");
				}
				catch(Exception)
				{
					return false;
				}
			}

			if(!receiverModified)
			{
				try
				{
					int amount = 20;
					int received = 0;

					if(args.Length == 2)
					{
						int value = int.Parse(args[1]);

						amount += value;
						received += value;
					}
					else
					{
						amount++;
						received++;
					}

					contents.Add($"{args[0]}#{amount}#0#{received}");
				}
				catch(Exception)
				{
					return false;
				}
			}

			Utilities.WriteLines(new StreamWriter(CookieRewards.path, false), contents, true);
			Utilities.SendMessage(channel, Resources.sent, nick, args.Length == 2 ? args[1] : "" + 1, args[0]);
			return true;
		}

		public override string[] Aliases()
		{
			return new string[] { "cookie" };
		}

		public override string Syntax()
		{
			return Resources.syntax_cookie;
		}
	}
}
