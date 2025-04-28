using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using SceneLoading.Core;
using SceneLoading.Data;

namespace SceneLoading.View
{
	internal sealed class LoadSceneButtonHandler : IEventHandler<MonoReferenceLoaded>
	{
		[Inject] private readonly IEventBus _eventBus;
		[Inject] private readonly SceneLoader _sceneLoader;
		
		public void Handle(MonoReferenceLoaded eventData)
		{
			if (eventData.Type != typeof(LoadSceneButtonReferenceData)) return;
			
			var reference = (LoadSceneButtonReferenceData)eventData.Reference;
			
			var button = reference.Button;
			var sceneName = reference.SceneName;

			button.onClick.RemoveAllListeners();
			
			button.onClick.AddListener(() => SceneLoader.Load(sceneName));
		}
	}
}