using bl4ckb0t.Util;
using bl4ckb0t.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace bl4ckb0t.ModuleAPI
{
    public class ModuleHandler
	{
		/// <summary>
		/// Defines wether private modules are currently being loaded
		/// </summary>
		internal static bool loadingPrivateModules = true;
		/// <summary>
		/// A list containing all modules
		/// </summary>
		public static readonly List<BaseModule> modules = new List<BaseModule>();

		/// <summary>
		/// Loads and initializes a specific module from the \modules subdirectory. If the file is not in said subdirectory, this method will abort without notice
		/// </summary>
		/// <param name="file">The complete file path of the module to load</param>
		/// <param name="client">The IrcClient to be passed to the module</param>
		public static void LoadModule(string file, Bot client)
		{
			if(!file.StartsWith(Path.Combine(Utilities.DataPath(), "modules").Replace("file:", "")))
				return;

			IEnumerator<BaseModule> enumerator = Assembly.LoadFrom(file).GetTypes().Where(t => t.IsSubclassOf(typeof(BaseModule))).Select(type => {
				return (BaseModule)Activator.CreateInstance(type, new string[] { file.Substring(file.LastIndexOf(Path.PathSeparator) + 1).Replace(".dll", "") }); //pass the name of the dll as the module name
			}).GetEnumerator();
			BaseModule module;

			enumerator.MoveNext();
			module = enumerator.Current;
			module.OnEnable(client);
			Add(module);
			enumerator.Dispose();
			Logger.Info("Loaded module " + module.Name);
		}

		/// <summary>
		/// Adds the specificed module to the list and sorts the list
		/// </summary>
		/// <param name="module">The module to add</param>
		public static void Add(BaseModule module)
		{
			modules.Add(module);
			modules.Sort((m1, m2) => {
				if(m1.RequiredPermissionLevel() == m2.RequiredPermissionLevel())
					return m1.Name.CompareTo(m2.Name);
				else
					return m1.RequiredPermissionLevel() < m2.RequiredPermissionLevel() ? 1 : -1;
			});
		}

		/// <summary>
		/// Sets the internal variable loadingPrivateModules to false, which cannot be reversed
		/// </summary>
		public static void FinishLoadingPrivateModules()
		{
			loadingPrivateModules = false;
		}

		/// <summary>
		/// Checks if a module is loaded
		/// </summary>
		/// <param name="name">The name of the module to check</param>
		/// <returns>true if the module is loaded, false otherwise</returns>
		public static bool IsLoaded(string name)
		{
			return GetModuleByName(name) != null;
		}

		/// <summary>
		/// Gets a module by its name
		/// </summary>
		/// <param name="name">The name of the module to get</param>
		/// <returns>The corresponding module, null if none could be found</returns>
		public static BaseModule GetModuleByName(string name)
		{
			foreach(BaseModule m in modules)
			{
				if(Utilities.EqualsIgnoreCase(m.Name, name))
					return m;
			}

			return null;
		}
	}
}
