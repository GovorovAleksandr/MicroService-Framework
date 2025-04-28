using ModuleRegistration.Public;
using SceneLoading.Core;
using SceneLoading.View;

namespace SceneLoading.Entry
{
	internal sealed class Entry : IGlobalModuleEntry
	{
		public void InstallBindings(IServiceLocator serviceLocator)
		{
			serviceLocator.AddInstanceFromNew<SceneLoader>();
			
			serviceLocator.AddInterfacesFromNew<LoadSceneButtonHandler>();
			serviceLocator.AddInterfacesFromNew<AutoSceneLoadHandler>();
			serviceLocator.AddInterfacesFromNew<ProgressSliderHandler>();
			serviceLocator.AddInterfacesFromNew<ConfirmSceneLoadingButtonHandler>();
			
			serviceLocator.AddInterfacesFromNew<EventHandler>();
		}
	}
}