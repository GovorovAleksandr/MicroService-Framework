using ModuleRegistration.Public;
using SceneLoading.Core;
using SceneLoading.View;

namespace SceneLoading.Entry
{
	internal sealed class Entry : IGlobalModuleEntry
	{
		public void InstallBindings(IServiceLocator serviceLocator)
		{
			serviceLocator.AddInstanceFromNew(() => new SceneLoader());
			
			serviceLocator.AddInterfacesFromNew(() => new LoadSceneButtonHandler());
			serviceLocator.AddInterfacesFromNew(() => new AutoSceneLoadHandler());
			serviceLocator.AddInterfacesFromNew(() => new ProgressSliderHandler());
			serviceLocator.AddInterfacesFromNew(() => new ConfirmSceneLoadingButtonHandler());
			
			serviceLocator.AddInterfacesFromNew(() => new EventHandler());
		}
	}
}