using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;

namespace SceneLoading.Core
{
	internal sealed class EventHandler : IEventHandler<LoadScene>, IEventHandler<ReloadScene>, IEventHandler<LoadSceneAsync>
	{
		[Inject] private readonly IEventBus _eventBus;
		[Inject] private readonly SceneLoader _sceneLoader;

		public void Handle(LoadScene eventData)
		{
			var sceneName = eventData.Name;
			SceneLoader.Load(sceneName);
		}

		public void Handle(ReloadScene eventData) => SceneLoader.Reload();
		
		public void Handle(LoadSceneAsync eventData)
		{
			var sceneName = eventData.Name;
			var autoConfirm = eventData.AutoConfirm;
			
			_sceneLoader.LoadAsync(sceneName, autoConfirm);
		}
	}
}