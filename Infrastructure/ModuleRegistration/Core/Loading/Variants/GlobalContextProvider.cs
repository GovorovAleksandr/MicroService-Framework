using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModuleRegistration.Core
{
	internal sealed class GlobalContextProvider
	{
		private const string PrefabPath = "";

		public GlobalContext GlobalContext { get; private set; }

		public void Initialize()
		{
			if (TryFind(out var context))
			{
				GlobalContext = context;
				return;
			}

			if (!TryLoadPrefab(out var prefab, out var unloader))
			{
				throw new Exception("ProjectContext prefab not found");
			}
			
			GlobalContext = CreateInstance(prefab);
			unloader.Invoke();
		}

		private static bool TryFind(out GlobalContext context)
		{
			context = Object.FindObjectOfType<GlobalContext>();

			return context != null;
		}

		private static bool TryLoadPrefab(out GlobalContext context, out Action unloader)
		{
			var prefabs = Resources.LoadAll<GlobalContext>(PrefabPath);

			if (prefabs.Length > 1) throw new Exception("There can be only one GlobalContext prefab");
			var prefab = prefabs.FirstOrDefault();
			
			context = prefab;

			unloader = () => Resources.UnloadUnusedAssets();
			
			return prefab != null;
		}

		private static GlobalContext CreateInstance(GlobalContext prefab)
		{
			var instance = Object.Instantiate(prefab);
			Object.DontDestroyOnLoad(instance);
			return instance;
		}
	}
}