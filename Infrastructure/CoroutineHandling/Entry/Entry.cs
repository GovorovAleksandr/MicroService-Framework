using CoroutineHandling.Core;
using ModuleRegistration.Public;

namespace CoroutineHandling.Entry
{
	internal sealed class Entry : IGlobalModuleEntry
	{
		public void InstallBindings(IServiceLocator serviceLocator)
		{
			serviceLocator.AddInterfacesFromNew(() => new CoroutineHandlerProvider());
			serviceLocator.AddInterfacesFromNew(() => new CoroutineRegistry());
			
			serviceLocator.AddInterfacesFromNew(() => new EventHandler());
			serviceLocator.AddInterfacesFromNew(() => new RequestHandler());
		}
	}
}