using System;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using System.IO;

namespace bl4ckb0t.Modules.CookieRewards
{
	public class CookieRewards: BaseModule
    {
		public static string path = new Uri(Path.Combine(Utilities.DataPath(), "cookies.txt")).LocalPath;

		public CookieRewards(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			if(!File.Exists(path))
				File.Create(path).Close();

			RegisterChannelCommand(new CookieCommand());
			RegisterChannelCommand(new CookiesCommand());
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage1, Resources.usage2, Resources.usage3, Resources.usage4 };
		}
	}
}
