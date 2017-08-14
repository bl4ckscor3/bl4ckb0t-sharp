using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System;
using System.IO;
using System.Net;

namespace bl4ckb0t.Modules.Evaluate
{
	public class Command : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			if(args.Length == 0)
				return false;

			Query(e, Utilities.ConcatAt(args));
			return true;
		}

		/// <summary>
		/// Queries WolframAlpha
		/// </summary>
		/// <param name="e">The class containing info about the event the command triggered</param>
		/// <param name="input">The query for WolframAlpha (text to send)</param>
		private void Query(PrivateMessage e, string input)
		{
			WebClient client = WebWrapper.NewClient();
			StreamReader reader = new StreamReader(client.OpenRead($"http://api.wolframalpha.com/v2/query?appid={Passwords.wolframapikey}&input={input.Replace("+", "%2B").Replace(" ", "+").Replace(",", ".")}"));
			string line = "";
			string channel = e.Source;

			try
			{
				//skipping lines until wanted line is reached
				while(!(line = reader.ReadLine()).Contains("position='200'"))
				{
					if(line.Contains("Appid missing"))
					{
						Utilities.SendMessage(channel, Resources.fail);
						Logger.Severe("Appid is missing. Something went horribly wrong.");
						client.Dispose();
						reader.Close();
						return;
					}
					else if(line.Contains("success='false'"))
					{
						Utilities.SendMessage(channel, Resources.notFound);
						client.Dispose();
						reader.Close();
						return;
					}
				}
			}
			catch(Exception)
			{
				Utilities.SendMessage(channel, Resources.fail);
				Logger.Warn("Result line could not be found.");
				client.Dispose();
				reader.Close();
				return;
			}

			try
			{
				//skipping lines to the line with the result
				while(!(line = reader.ReadLine()).Contains("plaintext"));
			}
			catch(Exception)
			{
				Utilities.SendMessage(channel, Resources.fail);
				Logger.Warn("Actual result not found.");
				client.Dispose();
				reader.Close();
				return;
			}

			client.Dispose();
			reader.Close();

			string result;

			try
			{
				result = line.Split('>')[1].Split('<')[0];
			}
			catch(Exception)
			{
				Logger.Info("Line was not formatted as expected. Sending raw line");
				result = line;
			}

			if(Utilities.Matches(result, "[0-9]+/[0-9]+.*") && !input.EndsWith("in decimal")) //the last condition is there to disallow infinite recursion
			{
				client.Dispose();
				reader.Close();
				Query(e, input + " in decimal"); //query wolfram alpha again, but now get the decimal value
				return;
			}

			Utilities.SendMessage(channel, result);
		}

		public override string[] Aliases()
		{
			return new string[] { "evaluate", "eval", "calculate", "calc" };
		}

		public override string Syntax()
		{
			return Resources.syntax;
		}
	}
}
