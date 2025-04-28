using CoroutineHandling.Core;
using ModuleRegistration.Public;

namespace CoroutineHandling.Entry
{
	internal sealed class Entry : IGlobalModuleEntry
	{
		public void InstallBindings(IServiceLocator serviceLocator)
		{
			serviceLocator.AddInterfacesFromNew<CoroutineHandlerProvider>();
			serviceLocator.AddInterfacesFromNew<CoroutineRegistry>();
			
			serviceLocator.AddInterfacesFromNew<EventHandler>();
			serviceLocator.AddInterfacesFromNew<RequestHandler>();
		}
	}
}