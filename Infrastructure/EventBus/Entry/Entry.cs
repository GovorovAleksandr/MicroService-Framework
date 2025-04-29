using ModuleRegistration.Public;

namespace EventBus.Entry
{
    internal sealed class Entry : IGlobalModuleEntry, IPrioritizedModuleEntry
    {
        public int Priority => -100;
        
        public void InstallBindings(IServiceLocator serviceLocator)
        {
            serviceLocator.AddInterfacesFromNew(() => new Core.EventBus());
        }
    }
}