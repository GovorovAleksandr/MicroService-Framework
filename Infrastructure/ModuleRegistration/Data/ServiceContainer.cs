using ModuleRegistration.Public;

namespace ModuleRegistration.Core
{
	internal class ServiceContainer
	{
		public readonly object Service;
		public readonly ServiceBindingFlags BindingFlags;

		public ServiceContainer(object service, ServiceBindingFlags bindingFlags)
		{
			Service = service;
			BindingFlags = bindingFlags;
		}
	}
}