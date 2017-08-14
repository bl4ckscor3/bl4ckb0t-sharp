using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System.Collections.Generic;

namespace bl4ckb0t.Modules.Leet
{
	public class Leet : BaseModule
	{
		private static List<char> normal = new List<char>();
		private static List<char> leet = new List<char>();

		public Leet(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			AddCharacter('a', '4');
			AddCharacter('e', '3');
			AddCharacter('g', '6');
			AddCharacter('l', '1');
			AddCharacter('o', '0');
			AddCharacter('s', '5');
			AddCharacter('t', '7');
			AddCharacter('z', '2');
			RegisterChannelCommand(new LeetCommand());
			RegisterChannelCommand(new UnleetCommand());
		}

		public override void OnDisable(Bot client)
		{
			normal.Clear();
			leet.Clear();
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2 };
		}

		/// <summary>
		/// Adds a normal character and its "leetified" version to the lists
		/// </summary>
		/// <param name="n">The normal character to add to the normal list</param>
		/// <param name="l">The "leetified" character to add to the leet list</param>
		private void AddCharacter(char n, char l)
		{
			normal.Add(n);
			leet.Add(l);
		}

		/// <summary>
		/// "Leetifies" a normal character
		/// </summary>
		/// <param name="c">The character to "leetify"</param>
		/// <returns>The "leetified" character, and the character itself if it can't be "leetified"</returns>
		public static char ToLeet(char c)
		{
			return normal.Contains(c) ? leet[normal.IndexOf(c)] : c;
		}

		/// <summary>
		/// Normalizes a "leetified"
		/// </summary>
		/// <param name="c">The "leetified" character to normalize</param>
		/// <returns>The normalized character, and the character itself if it can't be normalized</returns>
		public static char ToNormal(char c)
		{
			return leet.Contains(c) ? normal[leet.IndexOf(c)] : c;
		}
	}
}
