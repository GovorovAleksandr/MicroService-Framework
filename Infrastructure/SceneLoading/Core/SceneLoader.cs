using System.Collections;
using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading.Core
{
	internal sealed class SceneLoader
	{
		[Inject] private readonly IEventBus _eventBus;
		
		private static AsyncOperation _loadingOperation;
		private static bool IsLoading => _loadingOperation is { isDone: false };
		
		public static void Load(string sceneName)
		{
			if (IsLoading) return;
			_loadingOperation = SceneManager.LoadSceneAsync(sceneName);
		}

		public static void Reload()
		{
			if (IsLoading) return;
			var sceneName = SceneManager.GetActiveScene().name;
			Load(sceneName);
		}

		public void LoadAsync(string sceneName, bool autoConfirm)
		{
			if (IsLoading) return;
			_eventBus.Send(new StartCoroutine(LoadSceneRoutine(sceneName, autoConfirm)));
		}
		
		private IEnumerator LoadSceneRoutine(string sceneName, bool autoConfirm)
		{
			_loadingOperation = SceneManager.LoadSceneAsync(sceneName);
			_loadingOperation.allowSceneActivation = false;
			
			while (_loadingOperation.progress < 0.9f)
			{
				_eventBus.Send(new SceneLoadingProgressChanged(_loadingOperation.progress));
				yield return null;
			}

			_eventBus.Send(autoConfirm ? new SceneLoaded() : new SceneLoaded(Confirm));

			if (autoConfirm) Confirm();
			
			yield break;

			void Confirm()
			{
				_loadingOperation.allowSceneActivation = true;
				_loadingOperation = null;
			}
		}
	}
}