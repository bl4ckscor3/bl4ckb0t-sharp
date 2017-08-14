using bl4ckb0t.Logging;
using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp;
using System;
using System.IO;
using System.Net;

namespace bl4ckb0t.Modules
{
	public class ModuleManagementCommand : BaseChannelCommand
	{
		public override bool Exe(PrivateMessage e, string cmdName, string[] args)
		{
			string channel = e.Source;

			if(args.Length == 2)
			{
				string folder = new Uri(Path.Combine(Utilities.DataPath(), "modules")).LocalPath;

				switch(args[0])
				{
					case "enable":
						foreach(string s in Directory.GetFiles(folder))
						{
							string name = Path.GetFileName(s).Split('.')[0];

							if(Utilities.EqualsIgnoreCase(args[1], name))
							{
								if(!s.EndsWith(".disabled"))
								{
									Utilities.SendMessage(channel, Resources.moduleManagement_alreadyEnabled);
									return true;
								}

								try
								{
									File.Move(s, s.Replace(".dll.disabled", ".dll"));
								}
								catch(Exception ex)
								{
									Utilities.SendMessage(channel, Resources.moduleManagement_problemEnabling);
									Logger.Warn("Renaming the file did not work!");
									Logger.StackTrace(ex);
									return true;
								}

								try
								{
									ModuleHandler.LoadModule(s.Replace(".dll.disabled", ".dll"), Utilities.Client());
									Utilities.SendMessage(channel, Resources.moduleManagement_enabled);
								}
								catch(Exception ex)
								{
									Utilities.SendMessage(channel, Resources.moduleManagement_errorEnabling);
									Logger.Warn("There was an error while loading the module.");
									Logger.StackTrace(ex);
								}

								return true;
							}
						}

						Utilities.SendMessage(channel, Resources.moduleManagement_private);
						return true;
					case "disable":
						foreach(string s in Directory.GetFiles(folder))
						{
							string name = Path.GetFileName(s).Split('.')[0];

							if(Utilities.EqualsIgnoreCase(args[1], name))
							{
								if(s.EndsWith(".disabled"))
								{
									Utilities.SendMessage(channel, Resources.moduleManagement_alreadyDisabled);
									return true;
								}

								try
								{
									File.Move(s, s.Replace(".dll", ".dll.disabled"));
								}
								catch(Exception ex)
								{
									Utilities.SendMessage(channel, Resources.moduleManagement_problemDisabling);
									Logger.Warn("Renaming the file did not work:");
									Logger.StackTrace(ex);
									return true;
								}

								BaseModule m = ModuleHandler.GetModuleByName(name);
								
								ModuleHandler.modules.Remove(m);
								m.OnDisable(Utilities.Client());
								Utilities.SendMessage(channel, Resources.moduleManagement_disabled);
								return true;
							}
						}
						
						Utilities.SendMessage(channel, Resources.moduleManagement_private);
						return true;
					case "remove":
					case "delete":
						foreach(string s in Directory.GetFiles(folder))
						{
							string name = Path.GetFileName(s).Split('.')[0];

							if(Utilities.EqualsIgnoreCase(args[1], name))
							{
								try
								{
									File.Delete(s);
								}
								catch(Exception ex)
								{
									Utilities.SendMessage(channel, Resources.moduleManagement_problemRemoving);
									Logger.Warn("Deleting the file did not work!");
									Logger.StackTrace(ex);
									return true;
								}

								if(ModuleHandler.IsLoaded(name))
								{
									BaseModule m = ModuleHandler.GetModuleByName(name);

									ModuleHandler.modules.Remove(m);
									m.OnDisable(Utilities.Client());
								}

								Utilities.SendMessage(channel, Resources.moduleManagement_removed);
								return true;
							}
						}

						Utilities.SendMessage(channel, Resources.moduleManagement_private);
						return true;
					case "reload":
						Exe(e, cmdName, new string[] { "disable", args[1] });
						Exe(e, cmdName, new string[] { "enable", args[1] });
						return true;
					case "load":
						string dlFileName = Path.GetFileName(args[1].Contains("?") ? args[1].Substring(0, args[1].IndexOf('?')) : args[1]);
						string path = new Uri(Path.Combine(Utilities.DataPath(), "modules", dlFileName)).LocalPath;

						try
						{
							WebClient client = WebWrapper.NewClient();
							
							client.DownloadFile(args[1], path);
							client.Dispose();
							ModuleHandler.LoadModule(path, Utilities.Client());
						}
						catch(Exception ex)
						{
							Utilities.SendMessage(channel, Resources.moduleManagement_errorDownloading);
							Logger.Warn("An error occured while downloading the file, deleting it!");
							File.Delete(path);
							Logger.StackTrace(ex);
							return true;
						}

						Utilities.SendMessage(channel, Resources.moduleManagement_loaded);
						return true;
					default:
						return false;
				}
			}
			else
				return false;
		}

		public override string[] Aliases()
		{
			return new string[] { "module" };
		}

		public override string Syntax()
		{
			return Resources.moduleManagement_syntax;
		}
	}
}
