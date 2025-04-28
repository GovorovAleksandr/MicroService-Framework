using ModuleRegistration.Core;

namespace ModuleRegistration.Public
{
	internal interface IContextRegistryInternal : IContextRegistry
	{
		void RegisterLocator(IServiceLocator locator);
		bool TryRemoveLocator(IServiceLocator locator);
	}
}