using EventBus.Public;
using InternalEvents.Public;
using ModuleRegistration.Public;
using SceneLoading.Data;

namespace SceneLoading.Core
{
	internal sealed class AutoSceneLoadHandler : IEventHandler<MonoReferenceLoaded>
	{
		[Inject] private readonly IEventBus _eventBus;

		public void Handle(MonoReferenceLoaded eventData)
		{
			if (eventData.Type != typeof(AutoSceneLoaderReferenceData)) return;
			
			var reference = (AutoSceneLoaderReferenceData)eventData.Reference;
			
			var sceneName = reference.SceneName;
			var autoConfirm = reference.AutoConfirm;
			
			_eventBus.Send(new LoadSceneAsync(sceneName, autoConfirm));
		}
	}
}