using CoroutineHandling.Configs;
using ModuleRegistration.Public;
using UnityEngine;

namespace CoroutineHandling.Core
{
	internal class CoroutineHandlerProvider : ICoroutineHandlerProvider
	{
		[Inject] private readonly Config _config; 
		
		private CoroutineHandler _coroutineHandler;
		
		public CoroutineHandler Get()
		{
			return _coroutineHandler ??= Create();
		}

		private CoroutineHandler Create()
		{
			var prefab = _config.CoroutineHandlerPrefab;
			var handler = Object.Instantiate(prefab);

			Object.DontDestroyOnLoad(handler);

			return handler;
		}
	}
}